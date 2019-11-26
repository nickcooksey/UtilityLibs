using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICNCLib;
namespace CNCLib
{
    public class NcFile : INcFile
    {        

        public int Count
        {
            get { return data.Count; }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public INcElement this[int index] { get { return data[index]; } set { data[index] = value; } }

        List<INcElement> data;

        public NcFile()
        {
            data = new List<INcElement>();
        }
        public List<string> AsNcTextFile(INcMachine ncMachine)
        {
            var file = new List<string>();
            IMachinePosition prevPosition = ncMachine.ProgStartPosition;
            file.AddRange(GetHeader(ncMachine));
            foreach(var ent in data)
            {
                file.Add(ent.AsNcString(ncMachine ));
            }
            file.AddRange(GetFooter(ncMachine));
            file.AddRange(GetEndOfFile(ncMachine));

            return file;
        }
        List<string> GetHeader(INcMachine ncMachine)
        {
            try
            {
                var header = new List<string>();
                header.Add(ncMachine.MachineCode.ComStart + "Title" + ncMachine.MachineCode.ComEnd);
                return header;
            }
            catch (Exception)
            {

                throw;
            }
        }
        List<string> GetFooter(INcMachine ncMachine)
        {
            try
            {
                var footer = new List<string>();
                footer.Add(ncMachine.MachineCode.ComStart + "Footer" + ncMachine.MachineCode.ComEnd);
                return footer;
            }
            catch (Exception)
            {

                throw;
            }
        }
        List <string>GetEndOfFile(INcMachine ncMachine)
        {
            try
            {
                var eof = new List<string>();
                eof.Add(ncMachine.MachineCode.EndofProg);
                return eof;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int IndexOf(INcElement item)
        {
            return data.IndexOf(item);
        }

        public void Insert(int index, INcElement item)
        {
            data.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public void Add(INcElement item)
        {
            data.Add(item);
        }
        public void AddRange(IEnumerable<INcElement> ncLines)
        {
            data.AddRange(ncLines);
        }
        public void Clear()
        {
            data.Clear();
        }

        public bool Contains(INcElement item)
        {
            return data.Contains(item);
        }

        public void CopyTo(INcElement[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public bool Remove(INcElement item)
        {
            return data.Remove(item);
        }

        public IEnumerator<INcElement> GetEnumerator()
        {
            return new NcFileEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new NcFileEnumerator(this);
        }
    }
}
