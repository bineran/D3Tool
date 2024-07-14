
 
    
          
        public Point wpPoint { get; set; } = new Point();

      


                    if (!this.d3KeyState[Keys.ShiftKey])
                    {
                        objdm.KeyDown((int)Keys.ShiftKey);
                    }
                    if (LastEnum_通货 == Enum_通货.改造石)
                    {
                        objdm.MoveTo(wpPoint.X, wpPoint.Y);
                        Sleep(SleepTime * speed);
                        objdm.LeftClick();
                        Sleep(SleepTime * speed);
                    }
                    else
                    {
                        SingleTH(thPoints[enumth]);
                    }

                    break;
                default:
                    if (this.d3KeyState[Keys.ShiftKey])
                    {
                        objdm.KeyUp((int)Keys.ShiftKey);
                    }
                    SingleTH(thPoints[enumth]);
                    break;
            }
        }
        #endregion
        public int SleepTime = 10;
        public int speed = 5;
        public void SingleTH(Point point)
        {
            Sleep(SleepTime * speed);
            objdm.MoveR(point.X, point.Y);
            Sleep(SleepTime * speed);
            objdm.RightClick();
            Sleep(SleepTime * speed);
            objdm.MoveTo(wpPoint.X, wpPoint.Y);
            Sleep(SleepTime * speed);
            objdm.LeftClick();
            Sleep(SleepTime * speed);
        }
            

        
