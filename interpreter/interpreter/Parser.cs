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
                            Programms.Programm1(ch);
                            state = State.A;
                        }
                        else if (Char.IsDigit(ch))
                        {
                            Programms.Programm4(ch);
                            state = State.B;
                        }
                        else
                        {
                            switch (ch)
                            {
                                case ' ':
                                    Programms.Programm21();
                                    state = State.S;
                                    break;

                                case '=':
                                    Programms.Programm21();
                                    state = State.E;
                                    break;

                                case '+':
                                    Programms.Programm8();
                                    state = State.S;
                                    break;

                                case '-':
                                    Programms.Programm9();
                                    state = State.S;
                                    break;

                                case '*':
                                    Programms.Programm10();
                                    state = State.S;
                                    break;

                                case '/':
                                    Programms.Programm11();
                                    state = State.S;
                                    break;

                                case '<':
                                    Programms.Programm21();
                                    state = State.C;
                                    break;

                                case '>':
                                    Programms.Programm21();
                                    state = State.D;
                                    break;

                                case '[':
                                    Programms.Programm12();
                                    state = State.S;
                                    break;

                                case ']':
                                    Programms.Programm13();
                                    state = State.S;
                                    break;

                                case ';':
                                    Programms.Programm20();
                                    state = State.S;
                                    break;

                                case '(':
                                    Programms.Programm14();
                                    state = State.S;
                                    break;

                                case ')':
                                    Programms.Programm15();
                                    state = State.S;
                                    break;

                                case '{':
                                    Programms.Programm29();
                                    state = State.S;
                                    break;

                                case '}':
                                    Programms.Programm30();
                                    state = State.S;
                                    break;

                                case ',':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '!':
                                    Programms.Programm21();
                                    state = State.D;
                                    break;

                                case '\n':
                                    Programms.Programm24();
                                    state = State.S;
                                    break;

                                case '\0':
                                    Programms.Programm25();
                                    state = State.S;
                                    break;
                            }
                        }

                        break;

                    #endregion

                    #region Лексема или имя

                    case State.A:

                        if (Char.IsLetter(ch))
                        {
                            Programms.Programm2(ch);
                            state = State.A;
                        }
                        else if (Char.IsDigit(ch))
                        {
                            Programms.Programm2(ch); 
                            state = State.A;
                        }
                        else
                        {
                            switch (ch)
                            {
                                case ' ':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '=':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '+':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '-':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '*':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '/':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '<':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '>':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '[':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case ']':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case ';':
                                    Programms.Programm3();
                                    state = State.S;
                                    break;

                                case '(':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case ')':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '{':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '}':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ',':
                                    Programms.Programm3();
                                    state = State.S;
                                    break;

                                case '!':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '\n':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;

                                case '\0':
                                    Programms.Programm26();
                                    state = State.S;
                                    break;
                            }
                        }

                        break;

                    #endregion

                    #region Константа

                    case State.B:

                        if (Char.IsLetter(ch))
                        {
                            Programms.Programm28(ch);
                            state = State.S;
                        }
                        else if (Char.IsDigit(ch))
                        {
                            Programms.Programm5(ch);
                            state = State.B;
                        }
                        else
                        {
                            switch (ch)
                            {
                                case ' ':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '=':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '+':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '-':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '*':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '/':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '<':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '>':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '[':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ']':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case ';':
                                    Programms.Programm6();
                                    state = State.S;
                                    break;

                                case '(':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ')':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '{':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '}':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ',':
                                    Programms.Programm6();
                                    state = State.S;
                                    break;

                                case '!':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '\n':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;

                                case '\0':
                                    Programms.Programm27();
                                    state = State.S;
                                    break;
                            }
                        }

                        break;

                    #endregion

                    #region Оператор <= или <

                    case State.C:

                        if (Char.IsLetter(ch))
                        {
                            Programms.Programm23(ch);
                            state = State.S;
                        }
                        else if (Char.IsDigit(ch))
                        {
                            Programms.Programm23(ch);
                            state = State.S;
                        }
                        else
                        {
                            switch (ch)
                            {
                                case ' ':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '=':
                                    Programms.Programm16();
                                    state = State.S;
                                    break;

                                case '+':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '-':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '*':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '/':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '<':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '>':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '[':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ']':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ';':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '(':
                                    Programms.Programm23();
                                    state = State.S;
                                    break;

                                case ')':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '{':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '}':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ',':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '!':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '\n':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '\0':
                                    Programms.Programm23();
                                    state = State.S;
                                    break;
                            }
                        }

                        break;

                    #endregion

                    #region Оператор >= или >

                    case State.D:

                        if (Char.IsLetter(ch))
                        {
                            Programms.Programm22(ch);
                            state = State.S;
                        }
                        else if (Char.IsDigit(ch))
                        {
                            Programms.Programm22(ch);
                            state = State.S;
                        }
                        else
                        {
                            switch (ch)
                            {
                                case ' ':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '=':
                                    Programms.Programm17();
                                    state = State.S;
                                    break;

                                case '+':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '-':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '*':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '/':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '<':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '>':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '[':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ']':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ';':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '(':
                                    Programms.Programm22();
                                    state = State.S;
                                    break;

                                case ')':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '{':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '}':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ',':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '!':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '\n':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '\0':
                                    Programms.Programm22();
                                    state = State.S;
                                    break;
                            }
                        }

                        break;

                    #endregion

                    #region Оператор !=

                    case State.E:

                        if (Char.IsLetter(ch))
                        {
                            Programms.Programm28(ch);
                            state = State.S;
                        }
                        else if (Char.IsDigit(ch))
                        {
                            Programms.Programm28(ch);
                            state = State.S;
                        }
                        else
                        {
                            switch (ch)
                            {
                                case ' ':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '=':
                                    Programms.Programm19();
                                    state = State.S;
                                    break;

                                case '+':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '-':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '*':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '/':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '<':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '>':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '[':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ']':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ';':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '(':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ')':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '{':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '}':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ',':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '!':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '\n':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '\0':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;
                            }
                        }

                        break;

                    #endregion

                    #region Операторы == или =

                    case State.F:

                        if (Char.IsLetter(ch))
                        {
                            Programms.Programm7(ch);
                            state = State.S;
                        }
                        else if (Char.IsDigit(ch))
                        {
                            Programms.Programm7(ch);
                            state = State.S;
                        }
                        else
                        {
                            switch (ch)
                            {
                                case ' ':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '=':
                                    Programms.Programm18();
                                    state = State.S;
                                    break;

                                case '+':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '-':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '*':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '/':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '<':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '>':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '[':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ']':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ';':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '(':
                                    Programms.Programm7();
                                    state = State.S;
                                    break;

                                case ')':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '{':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '}':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case ',':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '!':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '\n':
                                    Programms.Programm28();
                                    state = State.S;
                                    break;

                                case '\0':
                                    Programms.Programm7();
                                    state = State.S;
                                    break;
                            }
                        }

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
