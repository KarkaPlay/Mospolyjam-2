using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Survivor : MonoBehaviour
{
    public float hp;
    public float water;
    public float food;
    public float sleep;
    public float sanity;
    public float temperature;
    private float speed;

    private Dictionary<string, float> modifiers = new()
    {
        { "hp",     1f },
        { "water",  1f },
        { "food",   1f },
        { "sleep",  1f },
        { "sanity", 1f },
    };
    
    public float Speed
    {
        get => speed;
        set
        {
            speed = value;
            navAgent.speed = value;
        }
    }

    public Inventory inventory;
    
    private enum State
    {
        Idle,
        WalkingAround,
        GoingToTarget,
        CollectingResource
    }
    [SerializeField] private State currentState;

    private float distanceToTarget;
    
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private GameObject targetObject;
    
    void Start()
    {
        SetupNavAgent();
        
        FindResource(AllResouceTypes.ResourceType.Wood, true);
    }

    private void SetupNavAgent()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }
    
    void Update()
    {
        Starve();
        CheckNeeds();
        CheckDistanceToTarget();
    }

    /// <summary>
    /// Постепенно уменьшает жизненные характеристики человека, используя модификаторы времени суток и другие (сделать)
    /// </summary>
    private void Starve()
    {
        // Чем больше модификатор, тем быстрее это тратится

        modifiers = TimeOfDay.currentDayTime switch
        {
            TimeOfDay.DayTime.Day => new()
            {
                { "hp", 0f },
                { "water", 0.5f },
                { "food", 0.5f },
                { "sleep", 0.5f },
                { "sanity", 0.3f }
            },
            TimeOfDay.DayTime.Evening => new()
            {
                { "hp", 0f },
                { "water", 0.4f },
                { "food", 0.4f },
                { "sleep", 1f },
                { "sanity", 0.5f }
            },
            TimeOfDay.DayTime.Night => new()
            {
                { "hp", 0f },
                { "water", 0.25f },
                { "food", 0.25f },
                { "sleep", 2f },
                { "sanity", 1f }
            },
            TimeOfDay.DayTime.Morning => new()
            {
                { "hp", 0f },
                { "water", 0.4f },
                { "food", 0.4f },
                { "sleep", 1f },
                { "sanity", 0.5f }
            },
            _ => modifiers
        };

        water -= modifiers[nameof(water)] * Time.deltaTime;
        food -= modifiers[nameof(food)] * Time.deltaTime;
        sleep -= modifiers[nameof(sleep)] * Time.deltaTime;
        sanity -= modifiers[nameof(sanity)] * Time.deltaTime;
    }

    private void CheckNeeds()
    {
        if (currentState is not (State.Idle or State.WalkingAround)) return;
        
        if (water < 30)
        {
            FindResource(AllResouceTypes.ResourceType.Water, true);
        }

        if (food < 40)
        {
            FindResource(AllResouceTypes.ResourceType.Food, true);
        }
    }

    private IEnumerator ChangeStateToIdle()
    {
        Debug.Log("Запущена ChangeStateToIdle");
        StopCoroutine(Walking());
        StopCoroutine(nameof(CollectingResource));
        speed = 0.5f;
        currentState = State.Idle;
        yield return new WaitForSeconds(3f);
        StartCoroutine(Walking());
    }

    /// <summary>
    /// Каждые несколько секунд задает для человека новую цель недалеко от него
    /// </summary>
    private IEnumerator Walking()
    {
        Debug.Log("Начали гулять");
        currentState = State.WalkingAround;
        Speed = 1f;
        
        while (currentState == State.WalkingAround)
        {
            Vector3 pos = transform.position;
            Vector3 randomPos = new Vector3(Random.Range(pos.x + 5, pos.x - 5), Random.Range(pos.y + 5, pos.y - 5));
            navAgent.SetDestination(randomPos);
            
            yield return new WaitForSeconds(Random.Range(3f, 8f));
        }
        Debug.Log("Закончили гулять");
    }
    
    /// <summary>
    /// Проверяет расстояние до цели. Если цель достигнута, вызывается OnNavAgentTargetReached(), а currentState
    /// меняется на CollectingResource
    /// </summary>
    private void CheckDistanceToTarget()
    {
        if (currentState is not State.GoingToTarget)
            return;
        
        distanceToTarget = navAgent.remainingDistance;
        
        if (!float.IsPositiveInfinity(distanceToTarget) && 
            navAgent.pathStatus==NavMeshPathStatus.PathComplete &&
            navAgent.remainingDistance==0)
        {
            navAgent.ResetPath();
            currentState = State.CollectingResource;
            OnNavAgentTargetReached();
        }
    }

    /// <summary>
    /// Вызывается, когда человек дошел до цели
    /// </summary>
    private void OnNavAgentTargetReached()
    {
        Debug.Log("Дошли до цели");
        StartCoroutine(CollectingResource(targetObject.GetComponent<Resource>()));
    }

    private IEnumerator CollectingResource(Resource resourceToCollect)
    {
        speed = 0;
        Debug.Log($"Начали собирать ресурс {resourceToCollect.resourceData.resourceType.ToString()}");
        
        while (currentState == State.CollectingResource)
        {
            Debug.Log("CollectingResource: ждем 2 секунды");
            yield return new WaitForSeconds(2f);
            Debug.Log($"CollectingResource: пытаемся собрать ресурс {resourceToCollect.resourceData.resourceType.ToString()}");
            switch (resourceToCollect.resourceData.resourceType)
            {
                case AllResouceTypes.ResourceType.Water:
                    CollectResource(resourceToCollect, ref water);
                    break;
                case AllResouceTypes.ResourceType.Food:
                    CollectResource(resourceToCollect, ref food);
                    break;
                default:
                    Debug.LogAssertion($"Сбор ресурса {resourceToCollect.resourceData.resourceType} не прописан в Survivor.CollectingResource()");
                    StartCoroutine(ChangeStateToIdle());
                    break;
            }
            Debug.Log("Собрали ресурс");
        }
        Debug.Log("Закончили собирать ресурс");
    }

    private void CollectResource(Resource resource, ref float survivorResource)
    {
        survivorResource += resource.oneTake;
        Debug.Log("Собрали ресурс");
        if (survivorResource > resource.resourceData.survivorNeed)
        {
            StartCoroutine(ChangeStateToIdle());
        }
        
        /*switch (resource.resourceData.resourceType)
        {
            case AllResouceTypes.ResourceType.Water:
                water += resource.oneTake;
                Debug.Log("Собрали ресурс");
                if (water > resource.resourceData.survivorNeed)
                {
                    StartCoroutine(ChangeStateToIdle());
                }
                break;
            case AllResouceTypes.ResourceType.Wood:
                Debug.LogAssertion("При сборе этого типа ничего не происходит");
                StartCoroutine(ChangeStateToIdle());
                break;
            case AllResouceTypes.ResourceType.Stone:
                Debug.LogAssertion("При сборе этого типа ничего не происходит");
                StartCoroutine(ChangeStateToIdle());
                break;
            case AllResouceTypes.ResourceType.Food:
                food += resource.oneTake;
                Debug.Log("Собрали ресурс");
                if (food > resource.resourceData.survivorNeed)
                {
                    StartCoroutine(ChangeStateToIdle());
                }
                break;
            default:
                Debug.LogAssertion("Попытка собрать неизвестный тип");
                break;
        }*/
        
    }

    /// <summary>
    /// Если currentState не CollectingResource и не GoingToTarget то для navAgent задается новая цель.
    /// currentState меняется на GoingToTarget
    /// </summary>
    /// <param name="target">GameObject, до которого будет идти человек</param>
    private void GoTo(GameObject target)
    {
        if (currentState is not State.CollectingResource and not State.GoingToTarget)
        {
            Speed = 2f;
            navAgent.SetDestination(target.transform.position);
            targetObject = target;
            currentState = State.GoingToTarget;
        }
    }

    /// <summary>
    /// Находит один рандомный объект ресурса типа _resourceType и, если надо, начинает движение человека к нему
    /// </summary>
    /// <param name="_resourceType">Тип искомого ресурса</param>
    /// <param name="needToGo">Нужно ли идти к найденному объекту (по умолчанию нет)</param>
    /// <returns>Возвращает первый попавшийся (не ближайший) Resource типа _resourceType</returns>
    private Resource FindResource(AllResouceTypes.ResourceType _resourceType, bool needToGo = false)
    {
        StopCoroutine(ChangeStateToIdle());
        StopCoroutine(Walking());
        Debug.Log($"Начали искать ресурс {_resourceType.ToString()}");
        
        var foundResource = ResourceObjects.Instance.FindResources(_resourceType)[0];

        if (needToGo)
        {
            GoTo(foundResource.gameObject);
        }
        
        return foundResource;
    }

    // Подошли к ресурсу
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            resource.playerIsNear = true;
        }
    }
    
    // Отошли от ресурса
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            resource.playerIsNear = false;
        }
    }
}
