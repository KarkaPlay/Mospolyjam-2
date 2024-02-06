using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Survivor : MonoBehaviour
{
    private float _hp;
    private float _water;
    private float _food;
    private float _sleep;
    private float _sanity;
    private float _temperature;
    private float _speed;

    public float HP
    {
        get => _hp;
        set
        {
            _hp = value;
            
            if (_hp < 0)
                _hp = 0;
            
            else if (_hp > 100)
                _hp = 100;
        }
    }
    public float Water
    {
        get => _water;
        set
        {
            _water = value;
            
            if (_water < 0)
                _water = 0;
            
            else if (_water > 100)
                _water = 100;
        }
    }
    public float Food
    {
        get => _food;
        set
        {
            _food = value;
            
            if (_food < 0)
                _food = 0;
            
            else if (_food > 100)
                _food = 100;
        }
    }
    public float Sleep
    {
        get => _sleep;
        set
        {
            _sleep = value;
            
            if (_sleep < 0)
                _sleep = 0;
            
            else if (_sleep > 100)
                _sleep = 100;
        }
    }
    public float Sanity
    {
        get => _sanity;
        set
        {
            _sanity = value;
            
            if (_sanity < 0)
                _sanity = 0;
            
            else if (_sanity > 100)
                _sanity = 100;
        }
    }
    public float Temperature
    {
        get => _temperature;
        set
        {
            _temperature = value;
            
            if (_temperature < 0)
                _temperature = 0;
            
            else if (_temperature > 100)
                _temperature = 100;
        }
    }
    public float Speed
    {
        get => _speed;
        set
        {
            _speed = value;
            navAgent.speed = value;
        }
    }

    private Dictionary<string, float> modifiers = new()
    {
        { "HP",     1f },
        { "Water",  1f },
        { "Food",   1f },
        { "Sleep",  1f },
        { "Sanity", 1f },
        { "Temperature", 1f }
    };

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

    public static Survivor Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetupNavAgent();
        SetupParameters();
        StartWalking();
    }

    /// <summary>
    /// Выставляет все жизненные параметры на 100
    /// </summary>
    private void SetupParameters()
    {
        HP = 100;
        Water = 100;
        Food = 100;
        Sleep = 100;
        Sanity = 100;
        Temperature = 100;
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

    /// <summary>
    /// Изменяет параметр номер paramNum на value
    /// </summary>
    /// <param name="paramNum">Номер параметра, который будет изменяться.
    /// <para>1 - HP</para>
    /// 2 - Water
    /// <para>3 - Food</para>
    /// 4 - Sleep
    /// <para>5 - Sanity</para>
    /// 6 - Temperature</param>
    /// <param name="value">Значение, на которое будет изменен параметр</param>
    public void ChangeParameter(int paramNum, float value)
    {
        if (paramNum == 1)
            HP += value;
        else if (paramNum == 2)
            Water += value;
        else if (paramNum == 3)
            Food += value;
        else if (paramNum == 4)
            Sleep += value;
        else if (paramNum == 5)
            Sanity += value;
        else if (paramNum == 6)
            Temperature += value;
        else
            Debug.LogAssertion($"Параметры: 1-6. Попытка изменить параметр №{paramNum}");
    }
    
    void Update()
    {
        Starve();
        CheckNeeds();
        CheckDistanceToTarget();
    }
    
    /// <summary>
    /// Постепенно уменьшает жизненные характеристики человека, используя модификаторы времени суток и другие
    /// </summary>
    private void Starve()
    {
        // Чем модификатор сеньше нуля, тем быстрее это тратится

        modifiers = TimeOfDay.currentDayTime switch
        {
            TimeOfDay.DayTime.Day => new()
            {
                { "HP",         -0f },
                { "Water",      -0.5f },
                { "Food",       -0.5f },
                { "Sleep",      -0.5f },
                { "Sanity",     -0.3f },
                { "Temperature", 0.5f }
            },
            TimeOfDay.DayTime.Evening => new()
            {
                { "HP",         -0f },
                { "Water",      -0.4f },
                { "Food",       -0.4f },
                { "Sleep",      -1f },
                { "Sanity",     -0.5f },
                { "Temperature", 0f }
            },
            TimeOfDay.DayTime.Night => new()
            {
                { "HP",         -0f },
                { "Water",      -0.25f },
                { "Food",       -0.25f },
                { "Sleep",      -2f },
                { "Sanity",     -1f },
                { "Temperature", -0.5f }
            },
            TimeOfDay.DayTime.Morning => new()
            {
                { "HP",         -0f },
                { "Water",      -0.4f },
                { "Food",       -0.4f },
                { "Sleep",      -1f },
                { "Sanity",     -0.5f },
                { "Temperature", 0f }
            },
            _ => modifiers
        };

        if (Water < 25)
            modifiers[nameof(HP)] -= 0.5f;

        if (Food < 35)
            modifiers[nameof(HP)] -= 0.5f;
        
        else if (Water > 80 && Food > 80)
            modifiers[nameof(HP)] += 1f;
        
        if (Sleep < 20)
            modifiers[nameof(HP)] -= 0.5f;
        
        if (Sanity < 25)
            modifiers[nameof(HP)] -= 0.5f;
        
        if (Temperature < 20)
            modifiers[nameof(HP)] -= 0.5f;
        
        // УДАЛИТЬ!!!
        float difficulty = 1;
        
        HP += modifiers[nameof(HP)] * Time.deltaTime * difficulty;
        Water += modifiers[nameof(Water)] * Time.deltaTime * difficulty;
        Food += modifiers[nameof(Food)] * Time.deltaTime * difficulty;
        Sleep += modifiers[nameof(Sleep)] * Time.deltaTime * difficulty;
        Sanity += modifiers[nameof(Sanity)] * Time.deltaTime * difficulty;
        Temperature += modifiers[nameof(Temperature)] * Time.deltaTime * difficulty;
    }

    /// <summary>
    /// Проверяет нужды человека в данный момент. Если какая-то характеристика меньше, чем нужно, человек ищет где её восполнить
    /// </summary>
    private void CheckNeeds()
    {
        if (currentState is not (State.Idle or State.WalkingAround)) return;
        
        if (Water < 30)
        {
            FindResource(AllResouceTypes.ResourceType.Вода, true);
        }

        if (Food < 40)
        {
            FindResource(AllResouceTypes.ResourceType.Плод, true);
        }

        if (Sleep < 30)
        {
            // Спим
        }

        if (Sanity < 30)
        {
            // Вырезает друзей, если они разблокированы
        }

        if (Temperature < 30)
        {
            FindResource(AllResouceTypes.ResourceType.Костер, true);
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
        _speed = 0;
        Debug.Log($"Начали собирать ресурс {resourceToCollect.resourceData.resourceType.ToString()}");
        
        while (currentState == State.CollectingResource)
        {
            yield return new WaitForSeconds(2f);
            switch (resourceToCollect.resourceData.resourceType)
            {
                case AllResouceTypes.ResourceType.Вода:
                    Debug.Log("Пытаемся собрать воду");
                    CollectResource(resourceToCollect, ref _water);
                    break;
                case AllResouceTypes.ResourceType.Плод:
                    CollectResource(resourceToCollect, ref _food);
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
        var foundResources = ResourceObjects.Instance.FindResources(_resourceType);
        
        Resource closestResource = foundResources[0];
        float closestDistance = Mathf.Infinity;
        foreach (var foundResource in foundResources)
        {
            var distanceToResource = Vector3.Distance(transform.position, foundResource.transform.position);
            if (distanceToResource < closestDistance)
            {
                closestDistance = distanceToResource;
                closestResource = foundResource;
            }
        }

        if (closestDistance != Mathf.Infinity)
        {
            if (needToGo)
            {
                GoTo(closestResource.gameObject);
            }

            return closestResource;
        }
        
        return null;
    }
}
