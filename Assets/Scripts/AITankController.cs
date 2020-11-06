using UnityEngine;
using System.Timers;
using System;

public class AITankController : TankController
{
    /*
        ********** первый вариант движения ии ***************
        танк будет двигаться к врагу (иногда останавливаясь) при этом
        постоянно виляя как змея, чтобы лазером "нащупать" врага.
        когда лазер показал, что да враг на прицеле, то важно сразу выстрелить
        остановится, снова нащупывая

        можно сделать две механики поиска:
        1) поиск на месте
        2) поиск в движении
        чередовать их отдавая преимущество второму
        отличаются они по сути только фактом нажатия на газ
        ********** второй вариант движения ии ****************
        танк генерирует waypoint в рандомном месте в пределе следующей ячейки
        лабиринта, куда ему надо добраться и едет туда
        когда танк "нащупывает" лазером цель, он также сразу выстреливает и переходит в режим прицеливания на время
        * таймер обновляется, когда танк находит цель
        * при истечении таймера, танк снова начинает движение
        Это в разы легче и делает бота не таким имбалансным
     */
    /*
       ниже реализован второй способ
     */
    private Timer searchTimer = new Timer(); 
    public enum movingType
    {
        turning,
        moving,
        search
    }
    private AITank tank;
    private Vector2 target; //следующий wayPoint
    public movingType type;
    private void Awake()
    {
        tank = GetComponent<AITank>();
    }
    void Start()
    {
        type = movingType.turning;
        trackStart();
        rotateSpeedMax = 150f;
        searchTimer.Elapsed += new ElapsedEventHandler(onSearchTimer);
    }

    void Update()
    {
        // state pattern
        Vector2 dir;
        if (type == movingType.turning) {
            dir = (Vector2)transform.position - target;
            turning(150f);
            //Debug.Log(((Vector2)transform.up - dir.normalized).magnitude);
            if (((Vector2)transform.up - dir.normalized).magnitude >= 1.999f)  //по непонятной причине, именно при этой длине оно обозначает, что вектор повернут
            {
                type = movingType.moving;
                return;
            }

        }
        if (type == movingType.moving) {
            dir = (Vector2)transform.position - target;
            if (!(((Vector2)transform.up - dir.normalized).magnitude >= 5f))  //по непонятной причине, именно при этой длине оно обозначает, что вектор повернут
                turning(150f);
            moveSpeed = (moveSpeed < moveSpeedMax) ? moveSpeed + moveAcceleration : moveSpeedMax;
            transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
            //Debug.Log(((Vector2)transform.position - target).magnitude);
            if (((Vector2)transform.position - target).magnitude < 1.2f)
            {
                GetNextWayPoint();
                try
                {
                    Destroy(tank.circles[0]);
                    tank.circles.RemoveAt(0);
                }
                catch (ArgumentOutOfRangeException excep)
                {
                    type = movingType.search;
                    Debug.LogWarning("circles are running out");
                }
                type = movingType.turning;
            }
        }
        if(type == movingType.search) { 
            try
            {
                Vector2 targetEnemy = tank.enemy.transform.position;
                dir = (Vector2)transform.position - targetEnemy;
                Vector3 c = new Vector3(0f, 0f, transform.up.x * dir.y - transform.up.y * dir.x); //результат векторного произведения
                                                                                                  //если направление по z у нас положительное, то поворачивать надо по часовой стрелке, иначе против
                int sign = -(int)c.normalized.z;
                transform.Rotate(0f, 0f, rotateSpeedMax * Time.deltaTime * sign);
            }
            catch (Exception ex) { }
        } 
    }

    public void GetNextWayPoint()
    {
        //указывает следующую цель
        if(tank.toPoint < tank.wayPoints.Count )
        {
            target = tank.wayPoints[tank.toPoint].GetComponent<Transform>().position;
            tank.toPoint++;
            // Debug.Log($"next is {target.x} -- {target.y}");
            type = movingType.turning;
            
        }
        else
        {
            tank.ChangeMovingType(movingType.search);
        }

    }

    public void updateSearchTimer()
    {
       searchTimer.Interval = 1000;
       searchTimer.Start();
    }

    void onSearchTimer(object source, ElapsedEventArgs e)
    {
        //tank.GetWay();
        type = movingType.turning;
        searchTimer.Stop();
    }
    
    void turning(float angle)
    {
        Vector2 dir = (Vector2)transform.position - target;
        Vector3 c = new Vector3(0f, 0f, transform.up.x * dir.y - transform.up.y * dir.x); //результат векторного произведения
        if (c.z == 0) c.z = 1;
        //если направление по z у нас положительное, то поворачивать надо по часовой стрелке, иначе против
        int sign = -(int)c.normalized.z;
        transform.Rotate(0f, 0f, angle * Time.deltaTime * sign);
    }

}
