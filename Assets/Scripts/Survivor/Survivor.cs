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

    /// <summary>
    /// Настраивает NavMeshAgent у человека
    /// </summary>
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

    // TODO: Добавить больше модификаторов
    /// <summary>
    /// Постепенно уменьшает жизненные характеристики человека, используя модификаторы времени суток и другие
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
        
        // УДАЛИТЬ!!!
        float difficulty = 1;
        
        water -= modifiers[nameof(water)] * Time.deltaTime * difficulty;
        food -= modifiers[nameof(food)] * Time.deltaTime * difficulty;
        sleep -= modifiers[nameof(sleep)] * Time.deltaTime * difficulty;
        sanity -= modifiers[nameof(sanity)] * Time.deltaTime * difficulty;
    }

    /// <summary>
    /// Проверяет нужды человека в данный момент. Если какая-то характеристика меньше, чем нужно, человек ищет где её восполнить
    /// </summary>
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

    /// <summary>
    /// Запускает корутину Walking()
    /// </summary>
    private void StartWalking()
    {
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
    /// Проверяет расстояние до цели. Если цель достигнута, вызывается OnNavAgentTargetReached()
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
            navAgent.SetDestination(transform.position);
            OnNavAgentTargetReached();
        }
    }

    /// <summary>
    /// Вызывается, когда человек дошел до цели. Начинает корутину CollectingResource
    /// </summary>
    private void OnNavAgentTargetReached()
    {
        Debug.Log("Дошли до цели");
        StartCoroutine(CollectingResource(targetObject.GetComponent<Resource>()));
    }

    /// <summary>
    /// Пока currentState = CollectingResource каждые 2 секунды пытается собрать ресурс у объекта
    /// </summary>
    /// <param name="resourceToCollect">Ресурс, который человек будет пытаться собрать</param>
    private IEnumerator CollectingResource(Resource resourceToCollect)
    {
        currentState = State.CollectingResource;
        speed = 0;
        Debug.Log($"Начали собирать ресурс {resourceToCollect.resourceData.resourceType.ToString()}");
        
        while (currentState == State.CollectingResource)
        {
            yield return new WaitForSeconds(2f);
            switch (resourceToCollect.resourceData.resourceType)
            {
                case AllResouceTypes.ResourceType.Water:
                    Debug.Log("Пытаемся собрать воду");
                    CollectResource(resourceToCollect, ref water);
                    break;
                case AllResouceTypes.ResourceType.Food:
                    CollectResource(resourceToCollect, ref food);
                    break;
                default:
                    Debug.LogAssertion($"Сбор ресурса {resourceToCollect.resourceData.resourceType} не прописан в Survivor.CollectingResource()");
                    StartWalking();
                    break;
            }
        }
        Debug.Log("Закончили собирать ресурс");
    }

    /// <summary>
    /// Запускает у ресурса resource.CollectResource() и добавляет oneTake к соответствующей характеристике человека
    /// </summary>
    /// <param name="resource">Ресурс, который человек пытается собрать</param>
    /// <param name="survivorResource">Характеристика человека, которая будет увеличиваться</param>
    private void CollectResource(Resource resource, ref float survivorResource)
    {
        if (resource.count > 0 && survivorResource < resource.resourceData.survivorNeed)
        {
            resource.CollectResource();
            survivorResource += resource.oneTake;
        }
        if (resource.count <= 0)
        {
            Destroy(resource.gameObject);
            StartWalking();
        }
        else if (survivorResource > resource.resourceData.survivorNeed)
        {
            StartWalking();
        }
    }

    /// <summary>
    /// Для navAgent задается новая цель, увеличиваеся скорость, currentState меняется на GoingToTarget
    /// </summary>
    /// <param name="target">GameObject, до которого будет идти человек</param>
    private void GoTo(GameObject target)
    {
        currentState = State.GoingToTarget;
        Speed = 2f;
        targetObject = target;
        navAgent.SetDestination(target.transform.position);
    }

    /// <summary>
    /// Находит один рандомный объект ресурса типа _resourceType и, если надо, начинает движение человека к нему
    /// </summary>
    /// <param name="_resourceType">Тип искомого ресурса</param>
    /// <param name="needToGo">Нужно ли идти к найденному объекту (по умолчанию нет)</param>
    /// <returns>Возвращает первый попавшийся (не ближайший) Resource типа _resourceType</returns>
    private Resource FindResource(AllResouceTypes.ResourceType _resourceType, bool needToGo = false)
    {
        if (currentState is State.CollectingResource or State.GoingToTarget)
            return null;
        
        StopCoroutine(Walking());
        Debug.Log($"Начали искать ресурс {_resourceType.ToString()}");
        
        // TODO: Искать не 0-й объект списка, а ближайший к человеку
        var foundResource = ResourceObjects.Instance.FindResources(_resourceType)[0];

        if (needToGo)
        {
            GoTo(foundResource.gameObject);
        }
        
        return foundResource;
    }
}
