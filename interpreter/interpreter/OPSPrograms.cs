using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter
{
    class OPSPrograms
    {
        private List<Unit> units;
        private bool isInt = false;
        private int indexJf;
        private int indexWf;

        public void Program1(ArrayList list)
        {
            indexJf = list.Count;
            var mark = new Mark();
            mark.name = "<jf>";
            list.Add(mark);
        }

        public void Program2(ArrayList list)
        {
            var mark = (list.ToArray()[indexJf] as Mark);
            mark.indexNextMark = list.Count;
            var markEj = new Mark();
            markEj.name = "<ej>";
            list.Add(markEj);
        }

        public void Program3(ArrayList list)
        {
            var mark = (list.ToArray()[indexJf] as Mark);
            mark.indexNextMark = list.Count;
            var markJ = new Mark();
            markJ.name = "<j>";
            list.Add(markJ);
        }

        public void Program4(ArrayList list)
        {
            indexWf = list.Count;
            var mark = new Mark();
            mark.name = "<wj>";
            list.Add(mark);
        }

        public void Program5(ArrayList list)
        {
            var mark = (list.ToArray()[indexJf] as Mark);
            mark.indexNextMark = list.Count;
            var markJ = new Mark();
            markJ.name = "<j>";
            mark.indexNextMark = indexWf;
            list.Add(markJ);
        }

        public void Program6(ArrayList list, string type)
        {
            units ??= new List<Unit>();
            if (type == "Int")
                isInt = true;
        }

        public void Program7(ArrayList list)
        {
            isInt = false;
            while (list.Contains("]"))
            {
                var index = list.IndexOf("]");
                list.RemoveAt(index);
                list.Insert(index, "m");
            }

            list.Add("<b>");
        }

        public void Program8(ArrayList list)
        {
            list.Add("dispose");
        }

        public Unit Program9(ArrayList list, string name)
        {
            if (isInt)
            {
                var unit = new Unit(name);

                if (!units.Where(q => q.name == name).Any())
                    units.Add(unit);
                else
                    throw new ArgumentException("Такое имя уже есть");

                list.Add(unit);
                return unit;
            }
            else 
                throw new ArgumentException("Объявлять переменные нельзя");
        }
    }
}
