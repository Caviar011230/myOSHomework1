using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        public Building myBuilding = new Building();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            myBuilding.newOp[0] = true;
            myBuilding.requestToUp[0] = true;
            button1.BackColor = Color.DeepSkyBlue;
        }

        private void emergency(int elevatorNum)
        {
            myBuilding.myElevator[elevatorNum].stop = true;
            myBuilding.myElevator[elevatorNum].stopTime = 10000;
            myBuilding.myElevator[elevatorNum].broken = true;
            int up = myBuilding.myElevator[elevatorNum].upDestination;
            int down = myBuilding.myElevator[elevatorNum].downDestination;
            for(int i = 0; i < 20; i++)
            {
                myBuilding.myElevator[elevatorNum].needToStop[i] = false;
                elevatorPutOutItem(i + 1, elevatorNum);
                
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!myBuilding.allFalse())
            {
                return;
            }//如果没有任何运行需求，则无动作
            myBuilding.clear();
            for(int i = 0; i < 5; i++)
            {
                if (myBuilding.myElevator[i].upDestination <= myBuilding.myElevator[i].nowAt && myBuilding.myElevator[i].isUp)
                {
                    myBuilding.myElevator[i].isUp = false;
                    if (myBuilding.myElevator[i].downDestination != -1)
                    {
                        myBuilding.myElevator[i].isDown = true;
                        if (myBuilding.myElevator[i].upDestination < myBuilding.myElevator[i].downDestination&&
                            (myBuilding.myElevator[i].needToStop[myBuilding.myElevator[i].upDestination]))
                        {
                            myBuilding.myElevator[i].downDestination = myBuilding.myElevator[i].upDestination;
                        }
                    }
                    myBuilding.myElevator[i].upDestination = -1;
                }
                if (myBuilding.myElevator[i].downDestination >= myBuilding.myElevator[i].nowAt && myBuilding.myElevator[i].isDown)
                {
                    myBuilding.myElevator[i].isDown = false;
                    if (myBuilding.myElevator[i].upDestination != -1)
                    {
                        myBuilding.myElevator[i].isUp = true;
                        if (myBuilding.myElevator[i].downDestination < myBuilding.myElevator[i].upDestination&&
                            (myBuilding.myElevator[i].needToStop[myBuilding.myElevator[i].downDestination]))
                        {
                            myBuilding.myElevator[i].upDestination = myBuilding.myElevator[i].downDestination;
                        }
                    }
                    myBuilding.myElevator[i].downDestination = -1;
                }
            }
            if (!myBuilding.myElevator[0].isUp&&!myBuilding.myElevator[0].isDown&&
                !myBuilding.myElevator[1].isUp && !myBuilding.myElevator[1].isDown &&
                !myBuilding.myElevator[2].isUp && !myBuilding.myElevator[2].isDown &&
                !myBuilding.myElevator[3].isUp && !myBuilding.myElevator[3].isDown &&
                !myBuilding.myElevator[4].isUp && !myBuilding.myElevator[4].isDown )
            {
                for (int i = 0; i < 20; i++)
                {
                    if (myBuilding.requestToUp[i])
                    {
                        if (myBuilding.myElevator[myBuilding.judge(i + 1, -1,true)].nowAt > i + 1)
                        {
                            myBuilding.myElevator[myBuilding.judge(i + 1, -1,true)].downDestination = i + 1;
                            myBuilding.myElevator[myBuilding.judge(i + 1, -1,true)].isDown = true;
                        }
                        if (myBuilding.myElevator[myBuilding.judge(i + 1, -1,true)].nowAt < i + 1)
                        {
                            myBuilding.myElevator[myBuilding.judge(i + 1, -1,true)].upDestination = i + 1;
                            myBuilding.myElevator[myBuilding.judge(i + 1, -1,true)].isUp = true;
                        }
                    }
                    if (myBuilding.requestToDown[i])
                    {
                        if (myBuilding.myElevator[myBuilding.judge(i + 1, -1,false)].nowAt > i + 1)
                        {
                            myBuilding.myElevator[myBuilding.judge(i + 1, -1,false)].downDestination = i + 1;
                            myBuilding.myElevator[myBuilding.judge(i + 1, -1,false)].isDown = true;
                        }
                        if (myBuilding.myElevator[myBuilding.judge(i + 1, -1,false)].nowAt < i + 1)
                        {
                            myBuilding.myElevator[myBuilding.judge(i + 1, -1,false)].upDestination = i + 1;
                            myBuilding.myElevator[myBuilding.judge(i + 1, -1,false)].isUp = true;
                        }
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (myBuilding.myElevator[i].upDestination == -1 && myBuilding.myElevator[i].downDestination == -1)
                {
                    myBuilding.myElevator[i].isUp = false;
                    myBuilding.myElevator[i].isDown = false;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (!myBuilding.myElevator[i].stop)
                {
                    if (myBuilding.myElevator[i].nowAt < 20 && myBuilding.myElevator[i].isUp)
                    {
                        myBuilding.myElevator[i].nowAt++;
                    }
                    else if (myBuilding.myElevator[i].nowAt > 1 && myBuilding.myElevator[i].isDown)
                    {
                        myBuilding.myElevator[i].nowAt--;
                    }
                    numChange(i);
                }
                if (!myBuilding.myElevator[i].isDown && !myBuilding.myElevator[i].isUp)
                {
                    if (myBuilding.myElevator[i].upDestination != -1 && myBuilding.myElevator[i].upDestination > myBuilding.myElevator[i].nowAt)
                    {
                        myBuilding.myElevator[i].isUp = true;
                    }
                    else if (myBuilding.myElevator[i].downDestination != -1 && myBuilding.myElevator[i].downDestination < myBuilding.myElevator[i].nowAt)
                    {
                        myBuilding.myElevator[i].isDown = true;
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (myBuilding.myElevator[i].stop)
                {
                    myBuilding.myElevator[i].stopTime--;
                    if (myBuilding.myElevator[i].stopTime == 0)
                    {
                        myBuilding.myElevator[i].stop = false;
                    }
                }
                else
                {
                    if (myBuilding.myElevator[i].isUp && myBuilding.myElevator[i].nowAt == myBuilding.myElevator[i].upDestination)
                    {
                        myBuilding.myElevator[i].upDestination = -1;
                        myBuilding.myElevator[i].isUp = false;
                        myBuilding.myElevator[i].stop = true;
                        myBuilding.myElevator[i].stopTime = 3;
                        elevatorPutOut(i);
                        if (myBuilding.myElevator[i].downDestination != -1)
                        {
                            myBuilding.myElevator[i].isDown = true;
                        }
                    }
                    if (myBuilding.myElevator[i].isDown && myBuilding.myElevator[i].nowAt == myBuilding.myElevator[i].downDestination//&&
                        /*!(myBuilding.myElevator[i].nowAt<myBuilding.myElevator[i].upDestination&&myBuilding.myElevator[i].isUp)*/)
                    {
                        myBuilding.myElevator[i].downDestination = -1;
                        myBuilding.myElevator[i].isDown = false;
                        myBuilding.myElevator[i].stop = true;
                        myBuilding.myElevator[i].stopTime = 3;
                        elevatorPutOut(i);
                        if (myBuilding.myElevator[i].upDestination != -1)
                        {
                            myBuilding.myElevator[i].isUp = true;
                        }
                    }
                    if (myBuilding.myElevator[i].needToStop[myBuilding.myElevator[i].nowAt - 1] ||//记得-1！！！
                        (!myBuilding.myElevator[i].isDown && myBuilding.requestToUp[myBuilding.myElevator[i].nowAt - 1]) ||
                        (!myBuilding.myElevator[i].isUp && myBuilding.requestToDown[myBuilding.myElevator[i].nowAt - 1]))
                    {
                        myBuilding.myElevator[i].stop = true;
                        myBuilding.myElevator[i].stopTime = 3;
                        myBuilding.myElevator[i].needToStop[myBuilding.myElevator[i].nowAt - 1] = false;
                        if (!myBuilding.myElevator[i].isDown)
                            myBuilding.requestToUp[myBuilding.myElevator[i].nowAt - 1] = false;
                        if (!myBuilding.myElevator[i].isUp)
                            myBuilding.requestToDown[myBuilding.myElevator[i].nowAt - 1] = false;
                    }//设置为stop的情况
                }
            }
            //以下是为电梯外部按上下按钮的情况进行分配
            for (int i = 0; i < 20; i++)
            {
                if (myBuilding.newOp[i])
                {
                    int temp;
                    if (myBuilding.requestToUp[i]) 
                        temp = myBuilding.judge(i + 1, -1,true);
                    else
                        temp = myBuilding.judge(i + 1, -1, true);
                    if ((myBuilding.myElevator[temp].upDestination != -1 && i + 1 > myBuilding.myElevator[temp].upDestination)||
                        (myBuilding.myElevator[temp].downDestination != -1 && i + 1 < myBuilding.myElevator[temp].downDestination)) 
                    {
                        if (myBuilding.myElevator[temp].upDestination != -1 && i + 1 > myBuilding.myElevator[temp].upDestination)
                        {
                            myBuilding.myElevator[temp].upDestination = i + 1;
                        }
                        if (myBuilding.myElevator[temp].downDestination != -1 && i + 1 < myBuilding.myElevator[temp].downDestination)
                        {
                            myBuilding.myElevator[temp].downDestination = i + 1;
                        }
                    }
                    else
                    {
                        if (i + 1 > myBuilding.myElevator[temp].nowAt)
                        {
                            if (i + 1 > myBuilding.myElevator[temp].upDestination)
                                myBuilding.myElevator[temp].upDestination = i + 1;
                            if (!myBuilding.myElevator[temp].isDown && (myBuilding.requestToUp[i]))
                                myBuilding.myElevator[temp].isUp = true;
                            else
                            {
                                if (!myBuilding.myElevator[temp].isUp)
                                    myBuilding.myElevator[temp].isDown = true;
                                if (i + 1 < myBuilding.myElevator[temp].downDestination || myBuilding.myElevator[temp].downDestination == -1)
                                    myBuilding.myElevator[temp].downDestination = i + 1;
                            }
                            if (myBuilding.requestToDown[i] && i + 1 < myBuilding.myElevator[temp].upDestination &&
                                (myBuilding.myElevator[temp].downDestination > i + 1 || myBuilding.myElevator[temp].downDestination == -1))
                            {
                                myBuilding.myElevator[temp].downDestination = i + 1;
                            }
                        }
                        else if (i + 1 < myBuilding.myElevator[temp].nowAt)
                        {
                            if (i + 1 < myBuilding.myElevator[temp].downDestination)
                                myBuilding.myElevator[temp].downDestination = i + 1;
                            if (!myBuilding.myElevator[temp].isUp && myBuilding.requestToDown[i])
                                myBuilding.myElevator[temp].isDown = true;
                            else
                            {
                                if (!myBuilding.myElevator[temp].isDown)
                                    myBuilding.myElevator[temp].isUp = true;
                                if (i + 1 > myBuilding.myElevator[temp].upDestination || myBuilding.myElevator[temp].upDestination == -1)
                                    myBuilding.myElevator[temp].upDestination = i + 1;
                            }
                            if (myBuilding.requestToUp[i] && i + 1 < myBuilding.myElevator[temp].downDestination &&
                                (myBuilding.myElevator[temp].upDestination > i + 1 || myBuilding.myElevator[temp].upDestination == -1))
                            {
                                myBuilding.myElevator[temp].upDestination = i + 1;
                            }
                        }
                        else
                        {
                            if (myBuilding.requestToDown[i] && i + 1 < myBuilding.myElevator[temp].upDestination &&
                               (myBuilding.myElevator[temp].downDestination > i + 1 || myBuilding.myElevator[temp].downDestination == -1))
                            {
                                myBuilding.myElevator[temp].downDestination = i + 1;
                            }
                            if (myBuilding.requestToUp[i] && i + 1 < myBuilding.myElevator[temp].downDestination &&
                                (myBuilding.myElevator[temp].upDestination > i + 1 || myBuilding.myElevator[temp].upDestination == -1))
                            {
                                myBuilding.myElevator[temp].upDestination = i + 1;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (!myBuilding.myElevator[i].isUp && !myBuilding.myElevator[i].isDown)
                {
                    if (myBuilding.myElevator[i].upDestination != -1)
                    {
                        myBuilding.myElevator[i].isUp = true;
                    }
                    else if (myBuilding.myElevator[i].downDestination != -1)
                    {
                        myBuilding.myElevator[i].isDown = true;
                    }
                }
                elevatorPutOut(i);
            }
            //以下检查目的地是否已被其他电梯抢先到达
            pictureBox1.Location = new Point(pictureBox1.Location.X, 550 - 27 * myBuilding.myElevator[0].nowAt);
            pictureBox2.Location = new Point(pictureBox2.Location.X, 550 - 27 * myBuilding.myElevator[1].nowAt);
            pictureBox3.Location = new Point(pictureBox3.Location.X, 550 - 27 * myBuilding.myElevator[2].nowAt);
            pictureBox4.Location = new Point(pictureBox4.Location.X, 550 - 27 * myBuilding.myElevator[3].nowAt);
            pictureBox5.Location = new Point(pictureBox5.Location.X, 550 - 27 * myBuilding.myElevator[4].nowAt);
            for (int i = 0; i < 20; i++)
            {
                myBuilding.newOp[i] = false;
            }
        }
        private void putOut(int floorNum, bool isUp, bool isDown)
        {
            switch (floorNum)
            {
                case 1:
                    button1.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 2:
                    if (!isDown)
                        button13.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button34.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 3:
                    if (!isDown)
                        button12.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button35.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 4:
                    if (!isDown)
                        button11.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button36.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 5:
                    if (!isDown)
                        button10.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button37.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 6:
                    if (!isDown)
                        button9.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button38.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 7:
                    if (!isDown)
                        button8.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button39.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 8:
                    if (!isDown)
                        button7.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button30.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 9:
                    if (!isDown)
                        button6.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button31.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 10:
                    if (!isDown)
                        button5.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button32.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 11:
                    if (!isDown)
                        button4.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button29.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 12:
                    if (!isDown)
                        button3.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button24.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 13:
                    if (!isDown)
                        button2.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button25.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 14:
                    if (!isDown)
                        button17.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button26.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 15:
                    if (!isDown)
                        button16.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button27.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 16:
                    if (!isDown)
                        button15.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button28.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 17:
                    if (!isDown)
                        button14.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button23.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 18:
                    if (!isDown)
                        button19.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button21.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 19:
                    if (!isDown)
                        button18.BackColor = Color.FromArgb(255, 240, 240, 240);
                    if (!isUp)
                        button22.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                case 20:
                    button20.BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;
                default:
                    break;
            }
        }
        private void elevatorPutOut(int numOfElevator)
        {
            putOut(myBuilding.myElevator[numOfElevator].nowAt, myBuilding.myElevator[numOfElevator].isUp,
                myBuilding.myElevator[numOfElevator].isDown);
            elevatorPutOutItem(myBuilding.myElevator[numOfElevator].nowAt, numOfElevator);
        }
        private void elevatorPutOutItem(int num,int numOfElevator)
        {
            switch (num)
            {
                case 1:
                    switch (numOfElevator)
                    {
                        case 0:
                            button33.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button91.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button123.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button132.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button138.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 2:
                    switch (numOfElevator)
                    {
                        case 0:
                            button54.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button60.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button109.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button116.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button124.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 3:
                    switch (numOfElevator)
                    {
                        case 0:
                            button53.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button61.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button110.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button117.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button125.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 4:
                    switch (numOfElevator)
                    {
                        case 0:
                            button52.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button62.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button111.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button118.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button126.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 5:
                    switch (numOfElevator)
                    {
                        case 0:
                            button51.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button63.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button112.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button119.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button127.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 6:
                    switch (numOfElevator)
                    {
                        case 0:
                            button40.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button90.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button122.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button131.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button137.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 7:
                    switch (numOfElevator)
                    {
                        case 0:
                            button50.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button64.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button113.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button120.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button133.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 8:
                    switch (numOfElevator)
                    {
                        case 0:
                            button49.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button65.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button114.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button128.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button134.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 9:
                    switch (numOfElevator)
                    {
                        case 0:
                            button48.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button66.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button115.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button129.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button135.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 10:
                    switch (numOfElevator)
                    {
                        case 0:
                            button47.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button67.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button121.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button130.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button136.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 11:
                    switch (numOfElevator)
                    {
                        case 0:
                            button87.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button57.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button106.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button77.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button83.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 12:
                    switch (numOfElevator)
                    {
                        case 0:
                            button86.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button58.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button107.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button78.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button84.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 13:
                    switch (numOfElevator)
                    {
                        case 0:
                            button85.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button59.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button108.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button89.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button88.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 14:
                    switch (numOfElevator)
                    {
                        case 0:
                            button100.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button41.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button92.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button42.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button74.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 15:
                    switch (numOfElevator)
                    {
                        case 0:
                            button99.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button43.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button93.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button68.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button75.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 16:
                    switch (numOfElevator)
                    {
                        case 0:
                            button98.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button44.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button101.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button69.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button76.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 17:
                    switch (numOfElevator)
                    {
                        case 0:
                            button97.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button45.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button102.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button70.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button79.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 18:
                    switch (numOfElevator)
                    {
                        case 0:
                            button96.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button46.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button103.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button71.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button80.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 19:
                    switch (numOfElevator)
                    {
                        case 0:
                            button95.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button55.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button104.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button72.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button81.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
                case 20:
                    switch (numOfElevator)
                    {
                        case 0:
                            button94.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 1:
                            button56.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 2:
                            button105.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 3:
                            button73.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                        case 4:
                            button82.BackColor = Color.FromArgb(255, 240, 240, 240);
                            break;
                    }
                    break;
            }
        }
        private void set(int eleNum, int flNum)
        {
            if (myBuilding.myElevator[eleNum].nowAt > flNum)
            {
                if (myBuilding.myElevator[eleNum].downDestination > flNum || myBuilding.myElevator[eleNum].downDestination == -1)
                {
                    myBuilding.myElevator[eleNum].downDestination = flNum;
                }
                if (!myBuilding.myElevator[eleNum].isUp)
                {
                    myBuilding.myElevator[eleNum].isDown = true;
                }
            }
            else if (myBuilding.myElevator[eleNum].nowAt < flNum)
            {
                if (myBuilding.myElevator[eleNum].upDestination < flNum || myBuilding.myElevator[eleNum].upDestination == -1)
                {
                    myBuilding.myElevator[eleNum].upDestination = flNum;
                }
                if (!myBuilding.myElevator[eleNum].isDown)
                {
                    myBuilding.myElevator[eleNum].isUp = true;
                }
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[1] = true;
            myBuilding.requestToUp[1] = true;
            button13.BackColor = Color.DeepSkyBlue;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[2] = true;
            myBuilding.requestToUp[2] = true;
            button12.BackColor = Color.DeepSkyBlue;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[3] = true;
            myBuilding.requestToUp[3] = true;
            button11.BackColor = Color.DeepSkyBlue;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[4] = true;
            myBuilding.requestToUp[4] = true;
            button10.BackColor = Color.DeepSkyBlue;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[5] = true;
            myBuilding.requestToUp[5] = true;
            button9.BackColor = Color.DeepSkyBlue;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[6] = true;
            myBuilding.requestToUp[6] = true;
            button8.BackColor = Color.DeepSkyBlue;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[7] = true;
            myBuilding.requestToUp[7] = true;
            button7.BackColor = Color.DeepSkyBlue;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[8] = true;
            myBuilding.requestToUp[8] = true;
            button6.BackColor = Color.DeepSkyBlue;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[9] = true;
            myBuilding.requestToUp[9] = true;
            button5.BackColor = Color.DeepSkyBlue;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[10] = true;
            myBuilding.requestToUp[10] = true;
            button4.BackColor = Color.DeepSkyBlue;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[11] = true;
            myBuilding.requestToUp[11] = true;
            button3.BackColor = Color.DeepSkyBlue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[12] = true;
            myBuilding.requestToUp[12] = true;
            button2.BackColor = Color.DeepSkyBlue;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[13] = true;
            myBuilding.requestToUp[13] = true;
            button17.BackColor = Color.DeepSkyBlue;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[14] = true;
            myBuilding.requestToUp[14] = true;
            button16.BackColor = Color.DeepSkyBlue;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[15] = true;
            myBuilding.requestToUp[15] = true;
            button15.BackColor = Color.DeepSkyBlue;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[16] = true;
            myBuilding.requestToUp[16] = true;
            button14.BackColor = Color.DeepSkyBlue;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[17] = true;
            myBuilding.requestToUp[17] = true;
            button19.BackColor = Color.DeepSkyBlue;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[18] = true;
            myBuilding.requestToUp[18] = true;
            button18.BackColor = Color.DeepSkyBlue;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[19] = true;
            myBuilding.requestToDown[19] = true;
            button20.BackColor = Color.DeepSkyBlue;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[18] = true;
            myBuilding.requestToDown[18] = true;
            button22.BackColor = Color.DeepSkyBlue;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[17] = true;
            myBuilding.requestToDown[17] = true;
            button21.BackColor = Color.DeepSkyBlue;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[16] = true;
            myBuilding.requestToDown[16] = true;
            button23.BackColor = Color.DeepSkyBlue;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[15] = true;
            myBuilding.requestToDown[15] = true;
            button28.BackColor = Color.DeepSkyBlue;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[14] = true;
            myBuilding.requestToDown[14] = true;
            button27.BackColor = Color.DeepSkyBlue;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[13] = true;
            myBuilding.requestToDown[13] = true;
            button26.BackColor = Color.DeepSkyBlue;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[12] = true;
            myBuilding.requestToDown[12] = true;
            button25.BackColor = Color.DeepSkyBlue;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[11] = true;
            myBuilding.requestToDown[11] = true;
            button24.BackColor = Color.DeepSkyBlue;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[10] = true;
            myBuilding.requestToDown[10] = true;
            button29.BackColor = Color.DeepSkyBlue;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[9] = true;
            myBuilding.requestToDown[9] = true;
            button32.BackColor = Color.DeepSkyBlue;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[8] = true;
            myBuilding.requestToDown[8] = true;
            button31.BackColor = Color.DeepSkyBlue;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[7] = true;
            myBuilding.requestToDown[7] = true;
            button30.BackColor = Color.DeepSkyBlue;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[6] = true;
            myBuilding.requestToDown[6] = true;
            button39.BackColor = Color.DeepSkyBlue;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[5] = true;
            myBuilding.requestToDown[5] = true;
            button38.BackColor = Color.DeepSkyBlue;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[4] = true;
            myBuilding.requestToDown[4] = true;
            button37.BackColor = Color.DeepSkyBlue;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[3] = true;
            myBuilding.requestToDown[3] = true;
            button36.BackColor = Color.DeepSkyBlue;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[2] = true;
            myBuilding.requestToDown[2] = true;
            button35.BackColor = Color.DeepSkyBlue;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            myBuilding.newOp[1] = true;
            myBuilding.requestToDown[1] = true;
            button34.BackColor = Color.DeepSkyBlue;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[0] = true;
            set(0, 1);
            button33.BackColor = Color.DeepSkyBlue;
        }
        private void button54_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[1] = true;
            set(0, 2);
            button54.BackColor = Color.DeepSkyBlue;
        }

        private void button53_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[2] = true;
            set(0, 3);
            button53.BackColor = Color.DeepSkyBlue;
        }

        private void button52_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[3] = true;
            set(0, 4);
            button52.BackColor = Color.DeepSkyBlue;
        }

        private void button51_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[4] = true;
            set(0, 5);
            button51.BackColor = Color.DeepSkyBlue;
        }

        private void button40_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[5] = true;
            set(0, 6);
            button40.BackColor = Color.DeepSkyBlue;
        }

        private void button50_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[6] = true;
            set(0, 7);
            button50.BackColor = Color.DeepSkyBlue;
        }

        private void button49_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[7] = true;
            set(0, 8);
            button49.BackColor = Color.DeepSkyBlue;
        }

        private void button48_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[8] = true;
            set(0, 9);
            button48.BackColor = Color.DeepSkyBlue;
        }

        private void button47_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[9] = true;
            set(0, 10);
            button47.BackColor = Color.DeepSkyBlue;
        }

        private void button87_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[10] = true;
            set(0, 11);
            button87.BackColor = Color.DeepSkyBlue;
        }

        private void button86_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[11] = true;
            set(0, 12);
            button86.BackColor = Color.DeepSkyBlue;
        }

        private void button85_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[12] = true;
            set(0, 13);
            button85.BackColor = Color.DeepSkyBlue;
        }

        private void button100_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[13] = true;
            set(0, 14);
            button100.BackColor = Color.DeepSkyBlue;
        }

        private void button99_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[14] = true;
            set(0, 15);
            button99.BackColor = Color.DeepSkyBlue;
        }

        private void button98_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[15] = true;
            set(0, 16);
            button98.BackColor = Color.DeepSkyBlue;
        }

        private void button97_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[16] = true;
            set(0, 17);
            button97.BackColor = Color.DeepSkyBlue;
        }

        private void button96_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[17] = true;
            set(0, 18);
            button96.BackColor = Color.DeepSkyBlue;
        }

        private void button95_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[18] = true;
            set(0, 19);
            button95.BackColor = Color.DeepSkyBlue;
        }

        private void button94_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].needToStop[19] = true;
            set(0, 20);
            button94.BackColor = Color.DeepSkyBlue;
        }

        private void button91_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[0] = true;
            set(1, 1);
            button91.BackColor = Color.DeepSkyBlue;
        }

        private void button60_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[1] = true;
            set(1, 2);
            button60.BackColor = Color.DeepSkyBlue;
        }

        private void button61_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[2] = true;
            set(1, 3);
            button61.BackColor = Color.DeepSkyBlue;
        }

        private void button62_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[3] = true;
            set(1, 4);
            button62.BackColor = Color.DeepSkyBlue;
        }

        private void button63_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[4] = true;
            set(1, 5);
            button63.BackColor = Color.DeepSkyBlue;
        }

        private void button90_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[5] = true;
            set(1, 6);
            button90.BackColor = Color.DeepSkyBlue;
        }

        private void button64_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[6] = true;
            set(1, 7);
            button64.BackColor = Color.DeepSkyBlue;
        }

        private void button65_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[7] = true;
            set(1, 8);
            button65.BackColor = Color.DeepSkyBlue;
        }

        private void button66_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[8] = true;
            set(1, 9);
            button66.BackColor = Color.DeepSkyBlue;
        }

        private void button67_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[9] = true;
            set(1, 10);
            button67.BackColor = Color.DeepSkyBlue;
        }

        private void button57_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[10] = true;
            set(1, 11);
            button57.BackColor = Color.DeepSkyBlue;
        }

        private void button58_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[11] = true;
            set(1, 12);
            button58.BackColor = Color.DeepSkyBlue;
        }

        private void button59_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[12] = true;
            set(1, 13);
            button59.BackColor = Color.DeepSkyBlue;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[13] = true;
            set(1, 14);
            button41.BackColor = Color.DeepSkyBlue;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[14] = true;
            set(1, 15);
            button43.BackColor = Color.DeepSkyBlue;
        }

        private void button44_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[15] = true;
            set(1, 16);
            button44.BackColor = Color.DeepSkyBlue;
        }

        private void button45_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[16] = true;
            set(1, 17);
            button45.BackColor = Color.DeepSkyBlue;
        }

        private void button46_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[17] = true;
            set(1, 18);
            button46.BackColor = Color.DeepSkyBlue;
        }

        private void button55_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[18] = true;
            set(1, 19);
            button55.BackColor = Color.DeepSkyBlue;
        }

        private void button56_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].needToStop[19] = true;
            set(1, 20);
            button56.BackColor = Color.DeepSkyBlue;
        }

        private void button123_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[0] = true;
            set(2, 1);
            button123.BackColor = Color.DeepSkyBlue;
        }

        private void button109_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[1] = true;
            set(2, 2);
            button109.BackColor = Color.DeepSkyBlue;
        }

        private void button110_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[2] = true;
            set(2, 3);
            button110.BackColor = Color.DeepSkyBlue;
        }

        private void button111_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[3] = true;
            set(2, 4);
            button111.BackColor = Color.DeepSkyBlue;
        }

        private void button112_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[4] = true;
            set(2, 5);
            button112.BackColor = Color.DeepSkyBlue;
        }

        private void button122_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[5] = true;
            set(2, 6);
            button122.BackColor = Color.DeepSkyBlue;
        }

        private void button113_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[6] = true;
            set(2, 7);
            button113.BackColor = Color.DeepSkyBlue;
        }

        private void button114_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[7] = true;
            set(2, 8);
            button114.BackColor = Color.DeepSkyBlue;
        }

        private void button115_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[8] = true;
            set(2, 9);
            button115.BackColor = Color.DeepSkyBlue;
        }

        private void button121_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[9] = true;
            set(2, 10);
            button121.BackColor = Color.DeepSkyBlue;
        }

        private void button106_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[10] = true;
            set(2, 11);
            button106.BackColor = Color.DeepSkyBlue;
        }

        private void button107_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[11] = true;
            set(2, 12);
            button107.BackColor = Color.DeepSkyBlue;
        }

        private void button108_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[12] = true;
            set(2, 13);
            button108.BackColor = Color.DeepSkyBlue;
        }

        private void button92_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[13] = true;
            set(2, 14);
            button92.BackColor = Color.DeepSkyBlue;
        }

        private void button93_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[14] = true;
            set(2, 15);
            button93.BackColor = Color.DeepSkyBlue;
        }

        private void button101_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[15] = true;
            set(2, 16);
            button101.BackColor = Color.DeepSkyBlue;
        }

        private void button102_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[16] = true;
            set(2, 17);
            button102.BackColor = Color.DeepSkyBlue;
        }

        private void button103_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[17] = true;
            set(2, 18);
            button103.BackColor = Color.DeepSkyBlue;
        }

        private void button104_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[18] = true;
            set(2, 19);
            button104.BackColor = Color.DeepSkyBlue;
        }

        private void button105_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].needToStop[19] = true;
            set(2, 20);
            button105.BackColor = Color.DeepSkyBlue;
        }

        private void button132_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[0] = true;
            set(3, 1);
            button132.BackColor = Color.DeepSkyBlue;
        }

        private void button116_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[1] = true;
            set(3, 2);
            button116.BackColor = Color.DeepSkyBlue;
        }

        private void button117_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[2] = true;
            set(3, 3);
            button117.BackColor = Color.DeepSkyBlue;
        }

        private void button118_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[3] = true;
            set(3, 4);
            button118.BackColor = Color.DeepSkyBlue;
        }

        private void button119_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[4] = true;
            set(3, 5);
            button119.BackColor = Color.DeepSkyBlue;
        }

        private void button131_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[5] = true;
            set(3, 6);
            button131.BackColor = Color.DeepSkyBlue;
        }

        private void button120_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[6] = true;
            set(3, 7);
            button120.BackColor = Color.DeepSkyBlue;
        }

        private void button128_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[7] = true;
            set(3, 8);
            button128.BackColor = Color.DeepSkyBlue;
        }

        private void button129_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[8] = true;
            set(3, 9);
            button129.BackColor = Color.DeepSkyBlue;
        }

        private void button130_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[9] = true;
            set(3, 10);
            button130.BackColor = Color.DeepSkyBlue;
        }

        private void button77_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[10] = true;
            set(3, 11);
            button77.BackColor = Color.DeepSkyBlue;
        }

        private void button78_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[11] = true;
            set(3, 12);
            button78.BackColor = Color.DeepSkyBlue;
        }

        private void button89_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[12] = true;
            set(3, 13);
            button89.BackColor = Color.DeepSkyBlue;
        }

        private void button42_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[13] = true;
            set(3, 14);
            button42.BackColor = Color.DeepSkyBlue;
        }

        private void button68_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[14] = true;
            set(3, 15);
            button68.BackColor = Color.DeepSkyBlue;
        }

        private void button69_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[15] = true;
            set(3, 16);
            button69.BackColor = Color.DeepSkyBlue;
        }

        private void button70_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[16] = true;
            set(3, 17);
            button70.BackColor = Color.DeepSkyBlue;
        }

        private void button71_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[17] = true;
            set(3, 18);
            button71.BackColor = Color.DeepSkyBlue;
        }

        private void button72_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[18] = true;
            set(3, 19);
            button72.BackColor = Color.DeepSkyBlue;
        }

        private void button73_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].needToStop[19] = true;
            set(3, 20);
            button73.BackColor = Color.DeepSkyBlue;
        }

        private void button138_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[0] = true;
            set(4, 1);
            button138.BackColor = Color.DeepSkyBlue;
        }

        private void button124_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[1] = true;
            set(4, 2);
            button124.BackColor = Color.DeepSkyBlue;
        }

        private void button125_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[2] = true;
            set(4, 3);
            button125.BackColor = Color.DeepSkyBlue;
        }

        private void button126_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[3] = true;
            set(4, 4);
            button126.BackColor = Color.DeepSkyBlue;
        }

        private void button127_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[4] = true;
            set(4, 5);
            button127.BackColor = Color.DeepSkyBlue;
        }

        private void button137_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[5] = true;
            set(4, 6);
            button137.BackColor = Color.DeepSkyBlue;
        }

        private void button133_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[6] = true;
            set(4, 7);
            button133.BackColor = Color.DeepSkyBlue;
        }

        private void button134_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[7] = true;
            set(4, 8);
            button134.BackColor = Color.DeepSkyBlue;
        }

        private void button135_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[8] = true;
            set(4, 9);
            button135.BackColor = Color.DeepSkyBlue;
        }

        private void button136_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[9] = true;
            set(4, 10);
            button136.BackColor = Color.DeepSkyBlue;
        }

        private void button83_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[10] = true;
            set(4, 11);
            button83.BackColor = Color.DeepSkyBlue;
        }

        private void button84_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[11] = true;
            set(4, 12);
            button84.BackColor = Color.DeepSkyBlue;
        }

        private void button88_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[12] = true;
            set(4, 13);
            button88.BackColor = Color.DeepSkyBlue;
        }

        private void button74_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[13] = true;
            set(4, 14);
            button74.BackColor = Color.DeepSkyBlue;
        }

        private void button75_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[14] = true;
            set(4, 15);
            button75.BackColor = Color.DeepSkyBlue;
        }

        private void button76_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[15] = true;
            set(4, 16);
            button76.BackColor = Color.DeepSkyBlue;
        }

        private void button79_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[16] = true;
            set(4, 17);
            button79.BackColor = Color.DeepSkyBlue;
        }

        private void button80_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[17] = true;
            set(4, 18);
            button80.BackColor = Color.DeepSkyBlue;
        }

        private void button81_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[18] = true;
            set(4, 19);
            button81.BackColor = Color.DeepSkyBlue;
        }

        private void button82_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].needToStop[19] = true;
            set(4, 20);
            button82.BackColor = Color.DeepSkyBlue;
        }

        private void button140_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].stop = false;
            if(!myBuilding.myElevator[0].broken)
                myBuilding.myElevator[0].stopTime = 0;
        }

        private void button139_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[0].stop = true;
        }

        private void button142_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].stop = true;
        }

        private void button144_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].stop = true;
        }

        private void button148_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].stop = true;
        }

        private void button146_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].stop = true;
        }

        private void button141_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[1].stop = false;
            if (!myBuilding.myElevator[1].broken)
                myBuilding.myElevator[1].stopTime = 0;
        }

        private void button143_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[2].stop = false;
            if (!myBuilding.myElevator[2].broken)
                myBuilding.myElevator[2].stopTime = 0;
        }

        private void button147_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[3].stop = false;
            if (!myBuilding.myElevator[3].broken)
                myBuilding.myElevator[3].stopTime = 0;
        }

        private void button145_Click(object sender, EventArgs e)
        {
            myBuilding.myElevator[4].stop = false;
            if (!myBuilding.myElevator[4].broken)
                myBuilding.myElevator[4].stopTime = 0;
        }

        private void button149_Click(object sender, EventArgs e)
        {
            emergency(0);
            button149.BackColor = Color.Yellow;
        }

        private void button150_Click(object sender, EventArgs e)
        {
            emergency(1);
            button150.BackColor = Color.Yellow;
        }

        private void button151_Click(object sender, EventArgs e)
        {
            emergency(2);
            button151.BackColor = Color.Yellow;
        }

        private void button152_Click(object sender, EventArgs e)
        {
            emergency(3);
            button152.BackColor = Color.Yellow;
        }

        private void button153_Click(object sender, EventArgs e)
        {
            emergency(4);
            button153.BackColor = Color.Yellow;
        }

        private void numChange(int elevatorNum)
        {
            switch (elevatorNum)
            {
                case 0:
                    pictureBox7.Image = imageList1.Images[myBuilding.myElevator[elevatorNum].nowAt % 10];
                    pictureBox6.Image = imageList1.Images[myBuilding.myElevator[elevatorNum].nowAt / 10];
                    break;
                case 1:
                    pictureBox8.Image = imageList1.Images[myBuilding.myElevator[elevatorNum].nowAt % 10];
                    pictureBox9.Image = imageList1.Images[myBuilding.myElevator[elevatorNum].nowAt / 10];
                    break;
                case 2:
                    pictureBox10.Image = imageList1.Images[myBuilding.myElevator[elevatorNum].nowAt % 10];
                    pictureBox11.Image = imageList1.Images[myBuilding.myElevator[elevatorNum].nowAt / 10];
                    break;
                case 3:
                    pictureBox12.Image = imageList1.Images[myBuilding.myElevator[elevatorNum].nowAt % 10];
                    pictureBox13.Image = imageList1.Images[myBuilding.myElevator[elevatorNum].nowAt / 10];
                    break;
                case 4:
                    pictureBox14.Image = imageList1.Images[myBuilding.myElevator[elevatorNum].nowAt % 10];
                    pictureBox15.Image = imageList1.Images[myBuilding.myElevator[elevatorNum].nowAt / 10];
                    break;
            }
        }
    }
}

