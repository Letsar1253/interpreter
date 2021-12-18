using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter
{
    /// <summary>
    /// Парсер строки из файла.
    /// Грубо говоря, это таблица переходов.
    /// </summary>
    class Parser
    {
        //строка для парсинга
        private string str;

        public Parser(string str)
        {
            this.str = str;

            //убираем из строки символ \r
            str = str.Replace(@"\r", "");
        }

        public void ParsString()
        {
            //Первоначальное состояние
            State state = State.S;

            //Строка представлена в char'ах
            var chars = str.ToCharArray();
            var programms = new Programms();
            foreach (var ch in chars)
            {
                switch (state)
                {
                    #region Начальное состояние

                    case State.S:

                        if (Char.IsLetter(ch))
                        {
                            state = State.A;
                        }
                        else if (Char.IsDigit(ch))
                        {
                            state = State.B;

                        }
                        else
                        {
                            switch (ch)
                            {
                                case ' ':
                                    break;

                                case '=':
                                    break;

                                case '+':
                                    break;

                                case '-':
                                    break;

                                case '*':
                                    break;

                                case '/':
                                    break;

                                case '<':
                                    break;

                                case '>':
                                    break;

                                case '[':
                                    break;

                                case ']':
                                    break;

                                case ';':
                                    break;

                                case '(':
                                    break;

                                case ')':
                                    break;

                                case '{':
                                    break;

                                case '}':
                                    break;

                                case ',':
                                    break;

                                case '!':
                                    break;

                                case '\n':
                                    break;

                            }
                        }

                        break;

                    #endregion

                    #region Лексема или имя

                    case State.A:

                        break;

                    #endregion

                    #region Константа

                    case State.B:

                        break;

                    #endregion

                    #region Оператор <= или <

                    case State.C:

                        break;

                    #endregion

                    #region Оператор >= или >

                    case State.D:

                        break;

                    #endregion

                    #region Оператор !=

                    case State.E:

                        break;

                    #endregion

                    #region Операторы == или =

                    case State.F:

                        break;

                    #endregion

                }
                
            }

        }

        private enum State
        {
            S, //Начальное состояние
            A, //Лексема или имя
            B, //Константа
            C, //Оператор <= или <
            D, //Оператор >= или >
            E, //Оператор !=
            F  //Операторы == или =
        }
    }
}
