using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Elevator
    {
        public Elevator()
        {
            for(int i = 0; i < 20; i++)
            {
                needToStop[i] = false;
            }
        }
        public bool up()
        {
            if (nowAt == 20)
            {
                return false;
            }
            nowAt++;
            return true;
        }
        public bool down()
        {
            if (nowAt == 1)
            {
                return false;
            }
            nowAt--;
            return true;
        }
        public bool allFalse()//
        {
            for(int i = 0; i < 20; i++)
            {
                if (needToStop[i])
                {
                    return true;
                }
            }
            return false;
        }
        public bool stop = false;//每次开门需要停一个时间间隔，为了在间隔时间跳过操作，需设置为true
        public bool running = false;//该项为true时，电梯在移动
        public bool isUp = false;//该项为true时，电梯上行
        public bool isDown = false;//该项为true时，电梯下行
        public int upDestination = -1;
        public int downDestination = -1;
        public bool doorOpen = false;//true时门开，false时门关
        public bool[] needToStop = new bool[20];//电梯内按钮，在第几楼停一下
        public int nowAt = 1;//当前所在楼层，默认为1，范围为1~20,一个时间单位最多只能移动一层。
    }
    public class Floor//一到20楼一楼一个对象
    {
        public Floor(int num = 1)
        {
            floorNum = num;
        }
        public int floorNum;//楼层号

        public bool needToUp = false;
        public bool needToDown = false;
        //需要上下行
    }
    public class Building
    {
        public Building()
        {

            for(int i = 0; i < 20; i++)
            {
                myFloor[i] = new Floor();
                myFloor[i].floorNum = i + 1;
            }//设置楼层号
            for(int i = 0; i < 5; i++)
            {
                myElevator[i] = new Elevator();
            }
            for(int i = 0; i < 20; i++)
            {
                requestToDown[i] = false;
                requestToUp[i] = false;//上下楼需求先设置为0
                newOp[i] = false;
            }
        }
        public bool allFalse()//都错误返回false，否则返回true
        {
            for(int i = 0; i < 20; i++)
            {
                if (requestToDown[i] || requestToUp[i])
                {
                    return true;
                }
            }
            for(int i = 0; i < 5; i++)
            {
                if (myElevator[i].allFalse())
                {
                    return true;
                }
            }
            return false;
        }
        public int judge(int floorNum)//判断应该使用哪部电梯
        {
            int[] cost = new int[5];
            for(int i = 0; i < 5; i++)
            {
                if(myElevator[i].nowAt == floorNum)
                {
                    return i;
                }
                else if (myElevator[i].nowAt > floorNum)
                {
                    if (myElevator[i].isUp == false)
                    {
                        cost[i] = myElevator[i].nowAt - floorNum;
                        for(int j = myElevator[i].nowAt; j > floorNum; j--)
                        {
                            if (requestToDown[j - 1])//设每次停下的时长为一个时间间隔,使用这种数组的数据时记得中括号内-1
                            {
                                cost[i]++;
                            }
                            else if (myElevator[i].needToStop[j])
                            {
                                cost[i]++;
                            }
                        }
                    }
                    else
                    {
                        int end = 20;
                        for(int j = 20; j >= floorNum; j--)
                        {
                            if (requestToDown[j - 1])//最高层的一个向下请求
                            {
                                end = j;
                                break;
                            }
                        }
                        cost[i] = (end - myElevator[i].nowAt) + (end - floorNum);
                        for(int j = myElevator[i].nowAt; j < end; j++)
                        {
                            if (requestToUp[j - 1])
                            {
                                cost[i]++;
                            }
                            else if (myElevator[i].needToStop[j])
                            {
                                cost[i]++;
                            }
                        }
                        for(int j = end; j > floorNum; j--)
                        {
                            if (requestToDown[j - 1])
                            {
                                cost[i]++;
                            }
                        }
                    }
                }
                else
                {
                    if (myElevator[i].isDown == false)
                    {
                        cost[i] = floorNum - myElevator[i].nowAt;
                        for (int j = myElevator[i].nowAt; j < floorNum; j++)
                        {
                            if (requestToUp[j - 1])//设每次停下的时长为一个时间间隔,使用这种数组的数据时记得中括号内-1
                            {
                                cost[i]++;
                            }
                        }
                    }
                    else
                    {
                        int end = 1;
                        for (int j = 1; j <= floorNum; j++)
                        {
                            if (requestToUp[j - 1])//最低层的一个向上请求
                            {
                                end = j;
                                break;
                            }
                        }
                        cost[i] = (myElevator[i].nowAt - end) + (floorNum - end);
                        for (int j = myElevator[i].nowAt; j > end; j--)
                        {
                            if (requestToDown[i - 1])
                            {
                                cost[i]++;
                            }
                        }
                        for (int j = end; j < floorNum; j++)
                        {
                            if (requestToDown[i - 1])
                            {
                                cost[i]++;
                            }
                        }
                    }
                }
            }
            int ans = 0;
            for(int i = 1; i < 5; i++)
            {
                if (cost[i] < cost[ans])
                {
                    ans = i;
                }
            }
            return ans;
        }
        public bool[] newOp = new bool[20];//第几层有新操作
        public Elevator[] myElevator = new Elevator[5];//大楼有五部电梯
        public Floor[] myFloor = new Floor[20];//大楼有20层
        public bool[] requestToUp = new bool[20];//i+1代表楼层数，true时说明该楼层请求上行
        public bool[] requestToDown = new bool[20];//i+1代表楼层数，true时说明该楼层请求下行
    }
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

}
//记录：2021.5.16下午 记得给电梯内部按钮也配上newOp数组
//回来解决电梯到达上行最终目的地后在下面楼层点击下行按钮居然还继续往上的问题
//2021.5.16晚 电梯到最高层后再下来会出问题
//周一及周二白天任务：debug 加显示屏 按钮被按后需求若未被解决则需变色