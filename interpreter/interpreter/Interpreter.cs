using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace interpreter
{
    /// <summary>
    /// Класс, который будет исполнять команды и выводить результат
    /// </summary>
    class Interpreter
    {
        private ArrayList ops;
        private bool startProgramBlock = false;
        private Unit previousUnit;
        private int previousConst;

        public Interpreter(ArrayList ops)
        {
            this.ops = ops;
        }

        public void Start()
        {
        object equalBuffer = null;
        object buffer1 = null, buffer2 = null;
            foreach (var item in ops)
            {
                if (!startProgramBlock)
                {
                    if (item is Unit)
                    {
                        if (previousUnit != null)
                        {
                            previousUnit.Init(Unit.structure.Variable);
                            previousUnit.Set(previousConst);
                        }
                        previousUnit = item as Unit;
                    }
                    else if (item is int)
                    {
                        previousConst = (int)item;
                    }
                    else if (item.ToString() == "=")
                    {
                        previousUnit.Init(Unit.structure.Variable);
                        previousUnit.Set(previousConst);
                        previousUnit = null;
                    }
                    else if (item.ToString() == "m")
                    {
                        previousUnit.Init(Unit.structure.Array, previousConst);
                        previousUnit = null;
                    }
                    else if (item.ToString() == "<b>")
                    {
                        startProgramBlock = true;
                        if (previousUnit != null)
                        {
                            previousUnit.Init(Unit.structure.Variable);
                            previousUnit.Set(previousConst);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Ошибка в объявлении");
                    }
                }
                else
                {
                    switch (item)
                    {
                        case Unit:
                            var unit = item as Unit;
                            if (equalBuffer == null)
                                equalBuffer = unit;
                            else if (buffer1 == null)
                                buffer1 = unit;
                            else
                                buffer2 = unit;
                            break;

                        case int:
                            int _const = (int)item;
                            if (buffer1 == null)
                                buffer1 = _const;
                            else
                                buffer2 = _const;
                            break;

                        case "+":
                            if (buffer1 is Unit && buffer2 is Unit)
                                buffer1 = (buffer1 as Unit) + (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                buffer1 = (buffer1 as Unit) + (int)buffer2;
                            else if (buffer2 is Unit && buffer1 is int)
                                buffer1 = (buffer2 as Unit) + (int)buffer1;
                            else
                                buffer1 = (int)buffer1 + (int)buffer2;
                            break;

                        case "=":
                            if (equalBuffer is Unit)
                                (equalBuffer as Unit).Set((int)buffer1);
                            break;

                        default:
                            //throw new ArgumentException("Ошибка в программном блоке");
                            break;
                    }
                }
            }
        }
    }
}
