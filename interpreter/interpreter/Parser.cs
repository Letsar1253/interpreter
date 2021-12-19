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
        //текущее состояние
        private State state;
        //экземпляр класса Programs
        private Programs programs;

        public Parser(string str)
        {
            this.str = str;

            //убираем из строки символ \r
            this.str = str.Replace("\r", String.Empty);

            //Добавляем конечный символ строки
            this.str += "\0";
        }

        public void ParsString()
        {
            //Первоначальное состояние
            state = State.S;

            //Строка представлена в char'ах
            var chars = str.ToCharArray();
            programs = new Programs();
            programs.NeedBack += OnNeedBack;
            foreach (var ch in chars)
            {
                FindProgram(ch);
            }

        }

        /// <summary>
        /// Получить список
        /// </summary>
        /// <returns></returns>
        public List<string> GetList()
        {
            return programs.GetList();
        }

        /// <summary>
        /// Обработчик события шага назад
        /// </summary>
        /// <param name="ch"></param>
        private void OnNeedBack(char ch)
        {
            FindProgram(ch);
        }

        /// <summary>
        /// Поиск программы по таблице переходов
        /// </summary>
        /// <param name="ch"></param>
        private void FindProgram(char ch)
        {
            switch (state)
            {
                #region Начальное состояние

                case State.S:

                    if (Char.IsLetter(ch))
                    {
                        state = State.A;
                        programs.Program1(ch);
                    }
                    else if (Char.IsDigit(ch))
                    {
                        state = State.B;
                        programs.Program4(ch);
                    }
                    else
                    {
                        switch (ch)
                        {
                            case ' ':
                                state = State.S;
                                programs.Program21();
                                break;

                            case '=':
                                state = State.F;
                                programs.Program21();
                                break;

                            case '+':
                                state = State.S;
                                programs.Program8();
                                break;

                            case '-':
                                state = State.S;
                                programs.Program9();
                                break;

                            case '*':
                                state = State.S;
                                programs.Program10();
                                break;

                            case '/':
                                state = State.S;
                                programs.Program11();
                                break;

                            case '<':
                                state = State.C;
                                programs.Program21();
                                break;

                            case '>':
                                state = State.D;
                                programs.Program21();
                                break;

                            case '[':
                                state = State.S;
                                programs.Program12();
                                break;

                            case ']':
                                state = State.S;
                                programs.Program13();
                                break;

                            case ';':
                                programs.Program20();
                                state = State.S;
                                break;

                            case '(':
                                state = State.S;
                                programs.Program14();
                                break;

                            case ')':
                                state = State.S;
                                programs.Program15();
                                break;

                            case '{':
                                state = State.S;
                                programs.Program29();
                                break;

                            case '}':
                                state = State.S;
                                programs.Program30();
                                break;

                            case ',':
                                state = State.S;
                                programs.Program31();
                                break;

                            case '!':
                                state = State.D;
                                programs.Program21();
                                break;

                            case '\n':
                                state = State.S;
                                programs.Program24();
                                break;

                            case '\0':
                                state = State.S;
                                programs.Program25();
                                break;

                            default:
                                state = State.S;
                                programs.Program28();
                                break;
                        }
                    }

                    break;

                #endregion

                #region Лексема или имя

                case State.A:

                    if (Char.IsLetter(ch))
                    {
                        state = State.A;
                        programs.Program2(ch);
                    }
                    else if (Char.IsDigit(ch))
                    {
                        state = State.A;
                        programs.Program2(ch);
                    }
                    else
                    {
                        switch (ch)
                        {
                            case ' ':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '=':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '+':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '-':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '*':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '/':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '<':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '>':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '[':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case ']':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case ';':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '(':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case ')':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '{':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '}':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ',':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '!':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '\n':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            case '\0':
                                state = State.S;
                                programs.Program26(ch);
                                break;

                            default:
                                state = State.S;
                                programs.Program28();
                                break;
                        }
                    }

                    break;

                #endregion

                #region Константа

                case State.B:

                    if (Char.IsLetter(ch))
                    {
                        state = State.S;
                        programs.Program28();
                    }
                    else if (Char.IsDigit(ch))
                    {
                        state = State.B;
                        programs.Program5(ch);
                    }
                    else
                    {
                        switch (ch)
                        {
                            case ' ':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '=':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '+':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '-':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '*':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '/':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '<':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '>':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '[':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ']':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case ';':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '(':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ')':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '{':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '}':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ',':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '!':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '\n':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            case '\0':
                                state = State.S;
                                programs.Program27(ch);
                                break;

                            default:
                                state = State.S;
                                programs.Program28();
                                break;
                        }
                    }

                    break;

                #endregion

                #region Оператор <= или <

                case State.C:

                    if (Char.IsLetter(ch))
                    {
                        state = State.S;
                        programs.Program23(ch);
                    }
                    else if (Char.IsDigit(ch))
                    {
                        state = State.S;
                        programs.Program23(ch);
                    }
                    else
                    {
                        switch (ch)
                        {
                            case ' ':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '=':
                                state = State.S;
                                programs.Program16();
                                break;

                            case '+':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '-':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '*':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '/':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '<':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '>':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '[':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ']':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ';':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '(':
                                state = State.S;
                                programs.Program23(ch);
                                break;

                            case ')':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '{':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '}':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ',':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '!':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '\n':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '\0':
                                state = State.S;
                                programs.Program23(ch);
                                break;

                            default:
                                state = State.S;
                                programs.Program28();
                                break;
                        }
                    }

                    break;

                #endregion

                #region Оператор >= или >

                case State.D:

                    if (Char.IsLetter(ch))
                    {
                        state = State.S;
                        programs.Program22(ch);
                    }
                    else if (Char.IsDigit(ch))
                    {
                        state = State.S;
                        programs.Program22(ch);
                    }
                    else
                    {
                        switch (ch)
                        {
                            case ' ':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '=':
                                state = State.S;
                                programs.Program17();
                                break;

                            case '+':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '-':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '*':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '/':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '<':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '>':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '[':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ']':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ';':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '(':
                                state = State.S;
                                programs.Program22(ch);
                                break;

                            case ')':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '{':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '}':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ',':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '!':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '\n':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '\0':
                                state = State.S;
                                programs.Program22(ch);
                                break;

                            default:
                                state = State.S;
                                programs.Program28();
                                break;
                        }
                    }

                    break;

                #endregion

                #region Оператор !=

                case State.E:

                    if (Char.IsLetter(ch))
                    {
                        state = State.S;
                        programs.Program28();
                    }
                    else if (Char.IsDigit(ch))
                    {
                        state = State.S;
                        programs.Program28();
                    }
                    else
                    {
                        switch (ch)
                        {
                            case ' ':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '=':
                                state = State.S;
                                programs.Program19();
                                break;

                            case '+':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '-':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '*':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '/':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '<':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '>':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '[':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ']':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ';':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '(':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ')':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '{':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '}':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ',':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '!':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '\n':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '\0':
                                state = State.S;
                                programs.Program28();
                                break;

                            default:
                                state = State.S;
                                programs.Program28();
                                break;
                        }
                    }

                    break;

                #endregion

                #region Операторы == или =

                case State.F:

                    if (Char.IsLetter(ch))
                    {
                        state = State.S;
                        programs.Program7(ch);
                    }
                    else if (Char.IsDigit(ch))
                    {
                        state = State.S;
                        programs.Program7(ch);
                    }
                    else
                    {
                        switch (ch)
                        {
                            case ' ':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '=':
                                state = State.S;
                                programs.Program18();
                                break;

                            case '+':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '-':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '*':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '/':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '<':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '>':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '[':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ']':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ';':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '(':
                                state = State.S;
                                programs.Program7(ch);
                                break;

                            case ')':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '{':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '}':
                                state = State.S;
                                programs.Program28();
                                break;

                            case ',':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '!':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '\n':
                                state = State.S;
                                programs.Program28();
                                break;

                            case '\0':
                                state = State.S;
                                programs.Program7(ch);
                                break;

                            default:
                                state = State.S;
                                programs.Program28();
                                break;
                        }
                    }

                    break;

                    #endregion

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
