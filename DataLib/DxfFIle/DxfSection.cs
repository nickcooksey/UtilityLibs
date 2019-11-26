using IDataLib;
using IGeometryLib;
using System;
using System.Collections.Generic;

namespace DataLib
{
    internal class DxfSection  
    {
        protected string sectionName;
        protected List<string> contents;

        internal DxfSection(string sectionName)
        {
            this.sectionName = sectionName;
            contents = new List<string>();
        }
        internal virtual List<string> AsString()
        {
            try
            {
                var result = new List<string>();
                result.AddRange(Header());
                result.AddRange(contents);
                result.AddRange(Footer());
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected List<string> Header()
        {
            try
            {
                var result = new List<string>();
                result.Add("0");
                result.Add("SECTION");
                result.Add("2");
                result.Add(sectionName);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected List<string> Footer()
        {
            try
            {
                var result = new List<string>();
                result.Add("0");
                result.Add("ENDSEC");
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    internal class DxfBlocksSection : DxfSection
    {
        private List<DxfBlock> dxfBlocks;
        internal void AddBlock(DxfBlock dxfBlock)
        {
            try
            {
                dxfBlocks.Add(dxfBlock);
                contents.AddRange(dxfBlock.AsString());
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal DxfBlocksSection() : base(sectionName: "BLOCKS")
        {
            dxfBlocks = new List<DxfBlock>();
        }
    }
    internal class DxfBlock
    {

        internal DxfBlock()
        {

        }
        internal List<string> AsString()
        {
            try
            {
                var content = new List<string>();

                return content;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    internal class DxfEntitiesSection : DxfSection
    {
        private List<IDwgEntity> entities;
        internal DxfEntitiesSection() : base(sectionName: "ENTITIES")
        {
            entities = new List<IDwgEntity>();
        }
        internal void AddEntity(IDwgEntity dwgEntity)
        {
            try
            {
                entities.Add(dwgEntity);
                contents.AddRange(dwgEntity.AsDXFString());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    internal class DxfObjectsSection : DxfSection
    {
        private List<DxfObject> objects;
        internal DxfObjectsSection() : base(sectionName: "OBJECTS")
        {
            objects = new List<DxfObject>();
        }
        internal void AddObject(DxfObject dwgEntity)
        {
            try
            {
                objects.Add(dwgEntity);
                contents.AddRange(dwgEntity.AsString());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    internal class DxfObject
    {
        public List<string> AsString()
        {
            try
            {
                var content = new List<string>();

                return content;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    internal class DxfHeaderSection : DxfSection
    {

        internal DxfHeaderSection() : base(sectionName: "HEADER")
        {

        }

    }
    internal class DxfTablesSection : DxfSection
    {
        private List<DxfTable> tables;
        internal void AddTable(DxfTable table)
        {
            tables.Add(table);
            contents.AddRange(table.AsString());
        }
        internal DxfTablesSection() : base(sectionName: "TABLES")
        {
            tables = new List<DxfTable>();
        }
    }
    internal class DxfClasssesSection : DxfSection
    {
        private List<DxfClass> dxfClasses;
        internal void AddClass(DxfClass dxfClass)
        {
            dxfClasses.Add(dxfClass);
            contents.AddRange(dxfClass.AsString());
        }
        internal DxfClasssesSection() : base(sectionName: "CLASSES")
        {
            dxfClasses = new List<DxfClass>();
        }
    }
    internal class DxfClass
    {

        internal List<string> AsString()
        {
            try
            {
                var content = new List<string>();
                return content;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
