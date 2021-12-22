using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter
{
    /// <summary>
    /// Класс, в котором будут произвоидиться ОПС генерации.
    /// Аналог таблицы ОПС генерации.
    /// </summary>
    class OPSGenerator
    {
        private List<string> list;

        //пустые квадратики 
        private List<string> generatorList = new();

        private ArrayList opsList = new();

        private List<string> magazine = new();

        public OPSGenerator(List<string> list)
        {
            this.list = list;

            this.list.RemoveAll(s => s == " " || s == "\n" | s == "\0");
        }

        public ArrayList GetOPS()
        {
            return opsList;
        }

        public void Generate()
        {
            generatorList.Add(String.Empty);
            magazine.Add("O");

            var dictionary = new Dictionary<string, Unit>();
            var opsPrograms = new OPSPrograms();

            while (generatorList.Count > 0)
            {
                var firstItem = generatorList.First();
                //Если первый элемент генератора НЕ пустой
                if (!String.IsNullOrEmpty(firstItem) && firstItem != null)
                {
                    switch (firstItem)
                    {
                        case "pr1":
                            opsPrograms.Program1(opsList);
                            RemoveAll();
                            break;

                        case "pr2":
                            opsPrograms.Program2(opsList);
                            RemoveAll();
                            break;

                        case "pr3":
                            opsPrograms.Program3(opsList);
                            RemoveAll();
                            break;

                        case "pr4":
                            opsPrograms.Program4(opsList);
                            RemoveAll();
                            break;

                        case "pr5":
                            opsPrograms.Program5(opsList);
                            RemoveAll();
                            break;

                        case "pr6":
                            opsPrograms.Program6(opsList, list.First());
                            RemoveAll();
                            break;

                        case "pr7":
                            opsPrograms.Program7(opsList);
                            RemoveAll();
                            break;

                        case "pr8":
                            opsPrograms.Program8(opsList);
                            RemoveAll();
                            break;

                        case "pr9":
                            var unit = opsPrograms.Program9(opsList, list.First());
                            dictionary.Add(list.First(), unit);
                            RemoveAll();
                            break;

                        case "a":
                            opsList.Add(dictionary.Where(s => s.Key == list.First()).First().Value);
                            RemoveAll();
                            break;

                        case "c":
                            var constStr = list.First();
                            opsList.Add(Convert.ToInt32(constStr));
                            RemoveAll();
                            break;

                        default:

                            //первый элемент из магазина
                            var magazineFirst = magazine.First();
                            //проверка первого элемента из магазина на терминал или нетерминал
                            if (magazineFirst != "O" && magazineFirst != "L" && magazineFirst != "N"
                                && magazineFirst != "H" && magazineFirst != "F" && magazineFirst != "P"
                                && magazineFirst != "W" && magazineFirst != "M" && magazineFirst != "I"
                                && magazineFirst != "S" && magazineFirst != "V" && magazineFirst != "T"
                                && magazineFirst != "B" && magazineFirst != "D" && magazineFirst != "E"
                                && magazineFirst != "C" && magazineFirst != String.Empty)
                            {
                                //Если условие выполняется, то это нетерминал
                                opsList.Add(list.First());
                                generatorList.Remove(firstItem);
                                magazine.RemoveAt(0);
                                list.RemoveAt(0);
                            }
                            else if (magazineFirst == String.Empty)
                            {
                                //Если условие не выполняется, то это терминал
                                opsList.Add(firstItem);
                                generatorList.Remove(firstItem);
                                magazine.RemoveAt(0);
                            }

                            break;
                    }
                }
                //Первый элемент генератора пустой
                else
                {
                    switch (magazine.First())
                    {
                        case "O": 
                            TerminalO(list.First());
                            break;

                        case "L":
                            TerminalL(list.First());
                            break;

                        case "N":
                            TerminalN(list.First());
                            break;

                        case "H":
                            TerminalH(list.First());
                            break;

                        case "F":
                            TerminalF(list.First());
                            break;

                        case "P":
                            TerminalP(list.First());
                            break;

                        case "W":
                            TerminalW(list.First());
                            break;

                        case "M":
                            TerminalM(list.First());
                            break;

                        case "I":
                            TerminalI(list.First());
                            break;

                        case "S":
                            TerminalS(list.First());
                            break;

                        case "V":
                            TerminalV(list.First());
                            break;

                        case "T":
                            TerminalT(list.First());
                            break;

                        case "B":
                            TerminalB(list.First());
                            break;

                        case "D":
                            TerminalD(list.First());
                            break;

                        case "E":
                            TerminalE(list.First());
                            break;

                        case "C":
                            TerminalC(list.First());
                            break;

                        default:
                            //Удаляем все
                            generatorList.Remove(firstItem);
                            magazine.RemoveAt(0);
                            list.RemoveAt(0);
                            break;
                    }
                }
            }
        }

        private void TerminalO(string item)
        {
            switch (item)
            {
                case "Int":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,"pr6");
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"O");
                    magazine.Insert(0,"L");
                    magazine.Insert(0,"Int");
                    break;

                case "{":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,"pr8");
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,"pr7");
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"}");
                    magazine.Insert(0,"C");
                    magazine.Insert(0,"F");
                    magazine.Insert(0,"{");
                    break;

                default:
                    throw new ArgumentException("Ошибка нетерминала O");
                    break;
            }
        }

        private void TerminalL(string item)
        {
            //первый символ непонятного нетерминала
            var ch = item.ToCharArray()[0];
            //Если первый символ - буква, то этот нетерминал - имя
            if (Char.IsLetter(ch))
            {
                //Делаем именно в таком порядке.
                //Сначала удаляем из генератора пустой символ,
                //и на его место заносим новые символы в начало,
                //из-за того, что заносим в начало, нужно добавлять с конца!!!
                generatorList.RemoveAt(0);
                generatorList.Insert(0,String.Empty);
                generatorList.Insert(0,String.Empty);
                generatorList.Insert(0,"pr9");
                //Тоже самое и с магазином
                magazine.RemoveAt(0);
                magazine.Insert(0,"N");
                magazine.Insert(0, "H");
                magazine.Insert(0, "a");
            }
            else
            {
                throw new ArgumentException("Ошибка нетерминала L");
            }
        }

        private void TerminalN(string item)
        {
            switch (item)
            {
                case ",":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0, String.Empty);
                    generatorList.Insert(0, "pr9");
                    generatorList.Insert(0, ",");
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, "N");
                    magazine.Insert(0, "H");
                    magazine.Insert(0, "a");
                    magazine.Insert(0, ",");
                    break;

                case ";":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, ";");
                    break;

                default:
                    throw new ArgumentException("Ошибка нетерминала N");
                    break;
            }
        }

        private void TerminalH(string item)
        {
            switch (item)
            {
                case "=":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,"=");
                    generatorList.Insert(0,"c");
                    generatorList.Insert(0, String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, String.Empty);
                    magazine.Insert(0,"c");
                    magazine.Insert(0,"=");
                    break;

                case "[":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,"m");
                    generatorList.Insert(0,"c");
                    generatorList.Insert(0, "[");
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, String.Empty);
                    magazine.Insert(0,"c");
                    magazine.Insert(0,"[");
                    break;

                default:
                    //лямда 
                    magazine.RemoveAt(0);
                    generatorList.RemoveAt(0);
                    break;
            }
        }

        private void TerminalF(string item)
        {
            switch (item)
            {

                default:
                    //первый символ непонятного нетерминала
                    var ch = item.ToCharArray()[0];
                    //Если первый символ - буква, то этот нетерминал - имя
                    if (Char.IsLetter(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0, "=");
                        generatorList.Insert(0, String.Empty);
                        generatorList.Insert(0, String.Empty);
                        generatorList.Insert(0, String.Empty);
                        generatorList.Insert(0, "a");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0, String.Empty);
                        magazine.Insert(0, "P");
                        magazine.Insert(0, "=");
                        magazine.Insert(0, "T");
                        magazine.Insert(0, "a");
                    }
                    else 
                        throw new ArgumentException("Ошибка нетерминала F");
                    break;
            }
        }

        private void TerminalP(string item)
        {
            switch (item)
            {
                case "+":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"M");
                    magazine.Insert(0,"I");
                    magazine.Insert(0,"V");
                    magazine.Insert(0,"+");
                    break;

                case "-":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0, "-'");
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"M");
                    magazine.Insert(0,"I");
                    magazine.Insert(0,"V");
                    magazine.Insert(0,"-");
                    break;

                case "(":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"W");
                    magazine.Insert(0,"I");
                    magazine.Insert(0,")");
                    magazine.Insert(0,"P");
                    magazine.Insert(0,"(");
                    break;

                default: 

                    //первый символ непонятного нетерминала
                    var ch = item.ToCharArray()[0];
                    //Если первый символ - буква, то этот нетерминал - имя
                    if (Char.IsLetter(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,"a");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0,"W");
                        magazine.Insert(0,"I");
                        magazine.Insert(0,"T");
                        magazine.Insert(0,"a");
                    }
                    //Если первый символ - цифра, то этот нетерминал - константа
                    else if (Char.IsDigit(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,"c");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0,"W");
                        magazine.Insert(0,"I");
                        magazine.Insert(0,"c");
                    }
                    //ошибка
                    else
                    {
                        throw new ArgumentException("Ошибка нетерминала P");
                    }

                    break;
            }
        }

        private void TerminalW(string item)
        {
            switch (item)
            {
                case "+":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,"+");
                    generatorList.Insert(0, String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, String.Empty);
                    magazine.Insert(0,"W");
                    magazine.Insert(0,"M");
                    magazine.Insert(0,"+");
                    break;

                case "-":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,"-");
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, "W");
                    magazine.Insert(0,"M");
                    magazine.Insert(0,"-");
                    break;

                default:
                    //лямда 
                    magazine.RemoveAt(0);
                    generatorList.RemoveAt(0);
                    break;
            }
        }

        private void TerminalM(string item)
        {
            switch (item)
            {
                case "+":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"I");
                    magazine.Insert(0,"V");
                    magazine.Insert(0,"+");
                    break;

                case "-":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,"-'");
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, "I");
                    magazine.Insert(0,"V");
                    magazine.Insert(0,"-");
                    break;

                case "(":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"I");
                    magazine.Insert(0,")");
                    magazine.Insert(0,"P");
                    magazine.Insert(0,"(");
                    break;

                default: 

                    //первый символ непонятного нетерминала
                    var ch = item.ToCharArray()[0];
                    //Если первый символ - буква, то этот нетерминал - имя
                    if (Char.IsLetter(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,"a");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0,"I");
                        magazine.Insert(0,"T");
                        magazine.Insert(0,"a");
                    }
                    //Если первый символ - цифра, то этот нетерминал - константа
                    else if (Char.IsDigit(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,"c");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0,"I");
                        magazine.Insert(0,"c");
                    }
                    //ошибка
                    else
                    {
                        throw new ArgumentException("Ошибка нетерминала M");
                    }

                    break;
            }
        }

        private void TerminalI(string item)
        {
            switch (item)
            {
                case "/":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,"/");
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"I");
                    magazine.Insert(0,"S");
                    magazine.Insert(0,"/");
                    break;

                case "*":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,"*");
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, "I");
                    magazine.Insert(0,"S");
                    magazine.Insert(0,"*");
                    break;

                default:
                    //лямда 
                    magazine.RemoveAt(0);
                    generatorList.RemoveAt(0);
                    break;
            }
        }

        private void TerminalS(string item)
        {
            switch (item)
            {
                case "+":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"V");
                    magazine.Insert(0,"+");
                    break;

                case "-":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,"-'");
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, "G");
                    magazine.Insert(0,"-");
                    break;

                case "(":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,")");
                    magazine.Insert(0,"P");
                    magazine.Insert(0,"(");
                    break;

                default: 

                    //первый символ непонятного нетерминала
                    var ch = item.ToCharArray()[0];
                    //Если первый символ - буква, то этот нетерминал - имя
                    if (Char.IsLetter(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,"a");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0,"T");
                        magazine.Insert(0,"a");
                    }
                    //Если первый символ - цифра, то этот нетерминал - константа
                    else if (Char.IsDigit(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0,"c");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0,"c");
                    }
                    //ошибка
                    else
                    {
                        throw new ArgumentException("Ошибка нетерминала S");
                    }

                    break;
            }
        }

        private void TerminalV(string item)
        {
            switch (item)
            {
                case "abs":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,"abs");
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,")");
                    magazine.Insert(0,"p");
                    magazine.Insert(0,"(");
                    magazine.Insert(0,"abs");
                    break;

                case "sqrt":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,"sqrt");
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,")");
                    magazine.Insert(0,"p");
                    magazine.Insert(0,"(");
                    magazine.Insert(0,"sqrt");
                    break;

                case "(":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,")");
                    magazine.Insert(0,"P");
                    magazine.Insert(0,"(");
                    break;

                default: 

                    //первый символ непонятного нетерминала
                    var ch = item.ToCharArray()[0];
                    //Если первый символ - буква, то этот нетерминал - имя
                    if (Char.IsLetter(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,"a");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0,"T");
                        magazine.Insert(0,"a");
                    }
                    //Если первый символ - цифра, то этот нетерминал - константа
                    else if (Char.IsDigit(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0,"c");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0,"c");
                    }
                    //ошибка
                    else
                    {
                        throw new ArgumentException("Ошибка нетерминала V");
                    }

                    break;
            }
        }

        private void TerminalT(string item)
        {
            switch (item)
            {
                case "[":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,"i");
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"]");
                    magazine.Insert(0,"P");
                    magazine.Insert(0,"[");
                    break;

                default:
                    //лямда 
                    magazine.RemoveAt(0);
                    generatorList.RemoveAt(0);
                    break;
            }
        }

        private void TerminalB(string item)
        {
            switch (item)
            {
                case "+":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"D");
                    magazine.Insert(0,"W");
                    magazine.Insert(0,"I");
                    magazine.Insert(0,"V");
                    magazine.Insert(0,"+");
                    break;

                case "-":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0, "-'");
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"D");
                    magazine.Insert(0,"W");
                    magazine.Insert(0,"I");
                    magazine.Insert(0,"V");
                    magazine.Insert(0,"-");
                    break;

                case "(":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"D");
                    magazine.Insert(0,"W");
                    magazine.Insert(0,"I");
                    magazine.Insert(0,")");
                    magazine.Insert(0,"P");
                    magazine.Insert(0,"(");
                    break;

                default: 

                    //первый символ непонятного нетерминала
                    var ch = item.ToCharArray()[0];
                    //Если первый символ - буква, то этот нетерминал - имя
                    if (Char.IsLetter(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,"a");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0,"D");
                        magazine.Insert(0,"W");
                        magazine.Insert(0,"I");
                        magazine.Insert(0,"T");
                        magazine.Insert(0,"a");
                    }
                    //Если первый символ - цифра, то этот нетерминал - константа
                    else if (Char.IsDigit(ch))
                    {
                        //Делаем именно в таком порядке.
                        //Сначала удаляем из генератора пустой символ,
                        //и на его место заносим новые символы в начало,
                        //из-за того, что заносим в начало, нужно добавлять с конца!!!
                        generatorList.RemoveAt(0);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,String.Empty);
                        generatorList.Insert(0,"c");
                        //Тоже самое и с магазином
                        magazine.RemoveAt(0);
                        magazine.Insert(0,"D");
                        magazine.Insert(0,"W");
                        magazine.Insert(0,"I");
                        magazine.Insert(0,"c");
                    }
                    //ошибка
                    else
                    {
                        throw new ArgumentException("Ошибка нетерминала B");
                    }

                    break;
            }
        }

        private void TerminalD(string item)
        {
            switch (item)
            {
                case "<=":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,"<=");
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"S");
                    magazine.Insert(0,"<=");
                    break;

                case ">=":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,">=");
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, "S");
                    magazine.Insert(0,">=");
                    break;

                case "<":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,"<");
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, "S");
                    magazine.Insert(0,"<");
                    break;

                case ">":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,">");
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, "S");
                    magazine.Insert(0,">");
                    break;

                case "==":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,"==");
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, "S");
                    magazine.Insert(0,"==");
                    break;

                case "!=":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0);
                    generatorList.Insert(0,"!=");
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0, "S");
                    magazine.Insert(0,"!=");
                    break;

                default:
                    throw new ArgumentException("Ошибка нетерминала D");
                    break;
            }
        }

        private void TerminalE(string item)
        {
            switch (item)
            {
                default:
                    //лямда 
                    magazine.RemoveAt(0);
                    generatorList.RemoveAt(0);
                    break;
            }
        }

        private void TerminalC(string item)
        {
            switch (item)
            {
                case ";":
                    //Делаем именно в таком порядке.
                    //Сначала удаляем из генератора пустой символ,
                    //и на его место заносим новые символы в начало,
                    //из-за того, что заносим в начало, нужно добавлять с конца!!!
                    generatorList.RemoveAt(0); 
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    generatorList.Insert(0,String.Empty);
                    //Тоже самое и с магазином
                    magazine.RemoveAt(0);
                    magazine.Insert(0,"C");
                    magazine.Insert(0,"F");
                    magazine.Insert(0,";");
                    break;

                default:
                    //лямда 
                    magazine.RemoveAt(0);
                    generatorList.RemoveAt(0);
                    break;
            }
        }

        private void RemoveAll()
        {
            generatorList.RemoveAt(0);
            magazine.RemoveAt(0);
            list.RemoveAt(0);
        }
    }
}
