using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AcBlockAtributeIncrement
{
    internal static class BlockExtensions
    {
        public static AttributeReference AddAttributeReferences(this BlockReference br, int index, string value)
        {
            BlockTableRecord obj = br.BlockTableRecord.GetObject<BlockTableRecord>();
            Transaction topTransaction = br.Database.TransactionManager.TopTransaction;
            AttributeReference attributeReference = null;
            AttributeDefinition[] array = obj.GetObjects<AttributeDefinition>().ToArray<AttributeDefinition>();
            for (int i = 0; i < (int)array.Length; i++)
            {
                AttributeDefinition attributeDefinition = array[i];
                AttributeReference attributeReference1 = new AttributeReference();
                attributeReference1.SetAttributeFromBlock(attributeDefinition, br.BlockTransform);
                Point3d position = attributeDefinition.Position;
                attributeReference1.Position = position.TransformBy(br.BlockTransform);
                if (attributeDefinition.Justify != AttachmentPoint.BaseLeft)
                {
                    position = attributeDefinition.AlignmentPoint;
                    attributeReference1.AlignmentPoint = position.TransformBy(br.BlockTransform);
                    attributeReference1.AdjustAlignment(br.Database);
                }
                if (attributeReference1.IsMTextAttribute)
                {
                    attributeReference1.UpdateMTextAttribute();
                }
                if (i == index)
                {
                    attributeReference1.TextString = value;
                    attributeReference = attributeReference1;
                }
                br.AttributeCollection.AppendAttribute(attributeReference1);
                topTransaction.AddNewlyCreatedDBObject(attributeReference1, true);
            }
            return attributeReference;
        }

        public static List<TextInfo> GetAttributesTextInfos(this BlockTableRecord btr)
        {
            return (
                from att in btr.GetObjects<AttributeDefinition>()
                select att.GetTextInfo()).ToList<TextInfo>();
        }

        public static ObjectId GetBlock(this BlockTable blockTable, string blockName)
        {
            string str;
            ObjectId @null;
            Database database = blockTable.Database;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(blockName);
            if (blockTable.Has(fileNameWithoutExtension))
            {
                return blockTable[fileNameWithoutExtension];
            }
            try
            {
                if (Path.GetExtension(blockName) == "")
                {
                    blockName = string.Concat(blockName, ".dwg");
                }
                str = (!File.Exists(blockName) ? HostApplicationServices.Current.FindFile(blockName, database, FindFileHint.Default) : blockName);
                blockTable.UpgradeOpen();
                using (Database database1 = new Database(false, true))
                {
                    database1.ReadDwgFile(str, FileShare.Read, true, null);
                    @null = blockTable.Database.Insert(Path.GetFileNameWithoutExtension(blockName), database1, true);
                }
            }
            catch
            {
                @null = ObjectId.Null;
            }
            return @null;
        }

        public static BlockTableRecord[] GetBlocksWithAttribute(this Database db)
        {
            Func<ObjectId, bool> func2 = null;
            RXClass @class = RXObject.GetClass(typeof(AttributeDefinition));
            return db.BlockTableId.GetObject<BlockTable>().GetObjects<BlockTableRecord>().Where<BlockTableRecord>((BlockTableRecord btr) => {
                if (btr.IsLayout || btr.IsAnonymous || btr.IsFromExternalReference || btr.IsFromOverlayReference)
                {
                    return false;
                }
                IEnumerable<ObjectId> objectIds = btr.Cast<ObjectId>();
                Func<ObjectId, bool> u003cu003e9_1 = func2;
                if (u003cu003e9_1 == null)
                {
                    Func<ObjectId, bool> func = (ObjectId id) => id.ObjectClass.IsDerivedFrom(@class);
                    Func<ObjectId, bool> func1 = func;
                    func2 = func;
                    u003cu003e9_1 = func1;
                }
                return objectIds.Any<ObjectId>(u003cu003e9_1);
            }).OrderBy<BlockTableRecord, string>((BlockTableRecord btr) => btr.Name).ToArray<BlockTableRecord>();
        }

        public static string GetEffectiveName(this BlockReference br)
        {
            if (!br.IsDynamicBlock)
            {
                return br.Name;
            }
            return br.DynamicBlockTableRecord.GetObject<BlockTableRecord>().Name;
        }

        public static int IndexOf(this BlockTableRecord btr, AttributeDefinition attDef)
        {
            return (
                from att in btr.GetObjects<AttributeDefinition>()
                where !att.Constant
                select att).ToList<AttributeDefinition>().FindIndex((AttributeDefinition x) => x == attDef);
        }

        public static void SetAttributeValue(this BlockReference br, int index, string value)
        {
            br.AttributeCollection[index].GetObject<AttributeReference>(OpenMode.ForWrite).TextString = value;
        }
    }
}
