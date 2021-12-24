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
        private int? index;

        public Interpreter(ArrayList ops)
        {
            this.ops = ops;
        }

        public void Start()
        {
            bool _bool = false;
            object buffer1 = null, buffer2 = null;
            ArrayList listWaitUnit = new();
            for (int i=0; i< ops.Count; i++)
            {
                if (!startProgramBlock)
                {
                    if (ops[i] is Unit)
                    {
                        if (previousUnit is not null)
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
                        if (previousUnit is not null)
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
                            if (buffer1 == null)
                                buffer1 = unit;
                            else if (buffer2 == null)
                                buffer2 = unit;
                            else
                            {
                                listWaitUnit.Add(buffer1);
                                buffer1 = buffer2;
                                buffer2 = unit;
                            }
                                
                            break;

                        case int:
                            int _const = (int)ops[i];
                            if (buffer1 == null)
                                buffer1 = _const;
                            else if (buffer2 == null)
                                buffer2 = _const;
                            else
                            {
                                listWaitUnit.Add(buffer1);
                                buffer1 = buffer2;
                                buffer2 = _const;
                            }
                            break;

                        case "+":
                            if (buffer2 == null && buffer1 == null)
                            {
                                buffer2 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            else if (buffer2 == null && buffer1 != null)
                            {
                                buffer2 = buffer1;
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            if (buffer1 == null)
                            {
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            
                            if (buffer1 is Unit && buffer2 is Unit)
                                buffer1 = (buffer1 as Unit) + (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                buffer1 = (buffer1 as Unit) + (int)buffer2;
                            else if (buffer2 is Unit && buffer1 is int)
                                buffer1 = (int)buffer1 + (buffer2 as Unit);
                            else
                                buffer1 = (int)buffer1 + (int)buffer2;
                            listWaitUnit.Add(buffer1);
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        case "-":
                            if (buffer2 == null && buffer1 == null)
                            {
                                buffer2 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            else if (buffer2 == null && buffer1 != null)
                            {
                                buffer2 = buffer1;
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            if (buffer1 == null)
                            {
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            if (buffer1 is Unit && buffer2 is Unit)
                                buffer1 = (buffer1 as Unit) - (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                buffer1 = (buffer1 as Unit) - (int)buffer2;
                            else if (buffer2 is Unit && buffer1 is int)
                                buffer1 = (int)buffer1 - (buffer2 as Unit);
                            else
                                buffer1 = (int)buffer1 - (int)buffer2;
                            listWaitUnit.Add(buffer1);
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        case "*":
                            if (buffer2 == null && buffer1 == null)
                            {
                                buffer2 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            else if (buffer2 == null && buffer1 != null)
                            {
                                buffer2 = buffer1;
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            if (buffer1 == null)
                            {
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            if (buffer1 is Unit && buffer2 is Unit)
                                buffer1 = (buffer1 as Unit) * (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                buffer1 = (buffer1 as Unit) * (int)buffer2;
                            else if (buffer2 is Unit && buffer1 is int)
                                buffer1 = (int)buffer1 * (buffer2 as Unit);
                            else
                                buffer1 = (int)buffer1 * (int)buffer2;
                            listWaitUnit.Add(buffer1);
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        case "/":
                            if (buffer2 == null && buffer1 == null)
                            {
                                buffer2 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            else if (buffer2 == null && buffer1 != null)
                            {
                                buffer2 = buffer1;
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            if (buffer1 == null)
                            {
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            if (buffer1 is Unit && buffer2 is Unit)
                                buffer1 = (buffer1 as Unit) / (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                buffer1 = (buffer1 as Unit) / (int)buffer2;
                            else if (buffer2 is Unit && buffer1 is int)
                                buffer1 = (int)buffer1 / (buffer2 as Unit);
                            else
                                buffer1 = (int)buffer1 / (int)buffer2;
                            listWaitUnit.Add(buffer1);
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        case "=":
                            if (buffer2 is Unit && buffer1 is Unit)
                            {
                                (buffer1 as Unit).Set(buffer2 as Unit);
                                buffer1 = null;
                                buffer2 = null;
                            }
                            else if (buffer2 is int && buffer1 is Unit)
                            {
                                (buffer1 as Unit).Set((int)buffer2);
                                buffer1 = null;
                                buffer2 = null;
                            }
                            else if (buffer1 is Unit)
                            {
                                (listWaitUnit.ToArray()[listWaitUnit.Count - 1] as Unit).Set(buffer1 as Unit);
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            else if (buffer1 is int)
                            {
                                (listWaitUnit.ToArray()[listWaitUnit.Count - 1] as Unit).Set((int)buffer1);
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            else
                            {
                                if (listWaitUnit.ToArray()[listWaitUnit.Count - 1] is Unit && listWaitUnit.Count>2)
                                    (listWaitUnit.ToArray()[listWaitUnit.Count - 2] as Unit).Set(listWaitUnit.ToArray()[listWaitUnit.Count - 1] as Unit);
                                else
                                    (listWaitUnit.ToArray()[listWaitUnit.Count - 2] as Unit).Set((int)listWaitUnit.ToArray()[listWaitUnit.Count - 1]);
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 2);
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            break;

                        case ">=":
                            if (buffer2 == null)
                            {
                                buffer2 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            if (buffer1 == null)
                            {
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            if (buffer1 is Unit && buffer2 is Unit)
                                _bool = (buffer1 as Unit) >= (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) >= (int)buffer2;
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) >= (int)buffer2;
                            else
                                _bool = (int)buffer1 >= (int)buffer2;
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        case "<=":
                            if (buffer2 == null)
                            {
                                buffer2 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            if (buffer1 == null)
                            {
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            if (buffer1 is Unit && buffer2 is Unit)
                                _bool = (buffer1 as Unit) <= (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) <= (int)buffer2;
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) <= (int)buffer2;
                            else
                                _bool = (int)buffer1 <= (int)buffer2;
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        case ">":
                            if (buffer2 == null)
                            {
                                buffer2 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            if (buffer1 == null)
                            {
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            if (buffer2 is null)
                            {
                                buffer2 = buffer1;
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                            }
                             
                            if (buffer1 is Unit && buffer2 is Unit)
                                _bool = (buffer1 as Unit) > (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) > (int)buffer2;
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) > (int)buffer2;
                            else
                                _bool = (int)buffer1 > (int)buffer2;
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        case "<":
                            if (buffer2 == null)
                            {
                                buffer2 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            if (buffer1 == null)
                            {
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            if (buffer1 is Unit && buffer2 is Unit)
                                _bool = (buffer1 as Unit) < (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) < (int)buffer2;
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) < (int)buffer2;
                            else
                                _bool = (int)buffer1 < (int)buffer2;
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        case "==":
                            if (buffer2 == null)
                            {
                                buffer2 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            if (buffer1 == null)
                            {
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            if (buffer1 is Unit && buffer2 is Unit)
                                _bool = (buffer1 as Unit) == (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) == (int)buffer2;
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) == (int)buffer2;
                            else
                                _bool = (int)buffer1 == (int)buffer2;
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        case "!=":
                            if (buffer2 == null)
                            {
                                buffer2 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            if (buffer1 == null)
                            {
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 1];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }
                            if (buffer1 is Unit && buffer2 is Unit)
                                _bool = (buffer1 as Unit) != (buffer2 as Unit);
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) != (int)buffer2;
                            else if (buffer1 is Unit && buffer2 is int)
                                _bool = (buffer1 as Unit) != (int)buffer2;
                            else
                                _bool = (int)buffer1 != (int)buffer2;
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        case "-'":
                            if (buffer2 == null)
                            {
                                if (buffer1 is Unit)
                                    (buffer1 as Unit).Change();
                                else
                                    buffer1 = (int) buffer1 * -1;
                            }
                            else
                            {
                                if (buffer2 is Unit)
                                    (buffer2 as Unit).Change();
                                else
                                    buffer2 = (int)buffer2 * -1;
                            }
                            break;

                        case Mark:
                            var mark = ops[i] as Mark;
                            switch (mark.name)
                            {
                                case "<jf>":
                                    if (!_bool)
                                    {
                                        i = mark.indexNextMark;
                                        buffer1 = null;
                                        buffer2 = null;
                                    }
                                    break;

                                case "<j>":
                                    if (mark.indexNextMark != 0)
                                    {
                                        i = mark.indexNextMark - 1;
                                        buffer1 = null;
                                        buffer2 = null;
                                    }
                                    break;
                            }
                            
                            break;

                        case "abs":
                            if (buffer1 is Unit)
                                buffer1 = (buffer1 as Unit).Abs();
                            else
                                buffer1 = Math.Abs((int)buffer1);
                            break;

                        case "sqrt":
                            if (buffer1 is Unit)
                                buffer1 = (buffer1 as Unit).Sqrt();
                            else
                                buffer1 = (int)Math.Sqrt((double)buffer1);
                            break;

                        case "w":
                            Console.WriteLine((buffer1 as Unit).Get());
                            buffer1 = null;
                            break;

                        case "r":
                            int val = Convert.ToInt32(Console.ReadLine());
                            (buffer1 as Unit).Set(val);
                            buffer1 = null;
                            break;

                        case "i":
                            if (buffer2 is int)
                                (buffer1 as Unit).SetCurrentIndex((int)buffer2);
                            else if (buffer2 is Unit)
                                (buffer1 as Unit).SetCurrentIndex(buffer2 as Unit);
                            else if (buffer1 is null && buffer2 is null)
                            {
                                (listWaitUnit.ToArray()[listWaitUnit.Count - 2] as Unit).SetCurrentIndex((int)listWaitUnit.ToArray()[listWaitUnit.Count - 1]);
                                buffer1 = listWaitUnit.ToArray()[listWaitUnit.Count - 2];
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 2);
                                listWaitUnit.RemoveAt(listWaitUnit.Count - 1);
                            }

                            buffer2 = null;
                            break;

                        case "dispose":
                            buffer1 = null;
                            buffer2 = null;
                            break;

                        default:
                            throw new ArgumentException("Ошибка в программном блоке");
                            break;
                    }
                }
            }
        }
    }
}
