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
            int nextMark;
            for (int i=0; i< ops.Count;)
            {
                if (!startProgramBlock)
                {
                    if (ops[i] is Unit)
                    {
                        if (previousUnit != null)
                        {
                            previousUnit.Init(Unit.structure.Variable);
                            previousUnit.Set(previousConst);
                        }
                        previousUnit = ops[i] as Unit;
                    }
                    else if (ops[i] is int)
                    {
                        previousConst = (int)ops[i];
                    }
                    else if (ops[i].ToString() == "=")
                    {
                        previousUnit.Init(Unit.structure.Variable);
                        previousUnit.Set(previousConst);
                        previousUnit = null;
                    }
                    else if (ops[i].ToString() == "m")
                    {
                        previousUnit.Init(Unit.structure.Array, previousConst);
                        previousUnit = null;
                    }
                    else if (ops[i].ToString() == "<b>")
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
                    switch (ops[i])
                    {
                        case Unit:
                            var unit = ops[i] as Unit;
                            if (equalBuffer == null)
                                equalBuffer = unit;
                            else if (buffer1 == null)
                                buffer1 = unit;
                            else
                                buffer2 = unit;
                            break;

                        case int:
                            int _const = (int)ops[i];
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

                        case "-":
                            if (buffer1 is Unit && buffer2 is Unit)
                                buffer1 = (buffer1 as Unit) - (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                buffer1 = (buffer1 as Unit) - (int)buffer2;
                            else if (buffer2 is Unit && buffer1 is int)
                                buffer1 = (buffer2 as Unit) - (int)buffer1;
                            else
                                buffer1 = (int)buffer1 - (int)buffer2;
                            break;

                        case "*":
                            if (buffer1 is Unit && buffer2 is Unit)
                                buffer1 = (buffer1 as Unit) * (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                buffer1 = (buffer1 as Unit) * (int)buffer2;
                            else if (buffer2 is Unit && buffer1 is int)
                                buffer1 = (buffer2 as Unit) * (int)buffer1;
                            else
                                buffer1 = (int)buffer1 * (int)buffer2;
                            break;

                        case "/":
                            if (buffer1 is Unit && buffer2 is Unit)
                                buffer1 = (buffer1 as Unit) / (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                buffer1 = (buffer1 as Unit) / (int)buffer2;
                            else if (buffer2 is Unit && buffer1 is int)
                                buffer1 = (buffer2 as Unit) / (int)buffer1;
                            else
                                buffer1 = (int)buffer1 / (int)buffer2;
                            break;

                        case "=":
                            if (equalBuffer is Unit)
                                (equalBuffer as Unit).Set((int)buffer1);
                            break;

                        case ">=":
                            if (buffer1 is Unit && buffer2 is Unit)
                                buffer1 = (buffer1 as Unit) >= (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                buffer1 = (buffer1 as Unit) >= (int)buffer2;
                            else if (buffer2 is Unit && buffer1 is int)
                                buffer1 = (buffer2 as Unit) >= (int)buffer1;
                            else
                                buffer1 = (int)buffer1 >= (int)buffer2;
                            break;

                        case "<=":
                            if (buffer1 is Unit && buffer2 is Unit)
                                buffer1 = (buffer1 as Unit) <= (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                buffer1 = (buffer1 as Unit) <= (int)buffer2;
                            else if (buffer2 is Unit && buffer1 is int)
                                buffer1 = (buffer2 as Unit) <= (int)buffer1;
                            else
                                buffer1 = (int)buffer1 <= (int)buffer2;
                            break;

                        case Mark:
                            var mark = ops[i] as Mark;
                            switch (mark.name)
                            {
                                case "<jf>":
                                    if (!(bool)buffer1)
                                        i = mark.indexNextMark;
                                    else
                                        nextMark = mark.indexNextMark;
                                    break;

                                case "<j>":

                                    break;
                            }
                            
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
