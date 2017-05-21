using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using acExcep = Autodesk.AutoCAD.Runtime.Exception;

namespace AcBlockAtributeIncrement
{
    internal static class AcadExtensions
    {
        public static ObjectId Add(this BlockTableRecord owner, Entity ent)
        {
            ObjectId id;
            if (owner == null)
            {
                throw new ArgumentNullException("Owner Block Table Record Null");
            }
            if (ent == null)
            {
                throw new ArgumentNullException("Entity Null");
            }
            Transaction topTr = owner.Database.TransactionManager.TopTransaction;
            if (topTr == null)
            {
                throw new acExcep(Autodesk.AutoCAD.Runtime.ErrorStatus.NotTopTransaction);
            }
            try
            {
                ObjectId objId1 = owner.AppendEntity(ent);
                topTr.AddNewlyCreatedDBObject(ent, true);
                id = objId1;
            }
            catch 
            {
                ent.Dispose();
                throw;
            }
            return id;
        }

        public static ObjectIdCollection Add(this BlockTableRecord owner, IEnumerable<Entity> entities)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("Owner Block Table Record Null");
            }
            if (entities == null)
            {
                throw new ArgumentNullException("Entity Null");
            }
            Transaction toptr = owner.Database.TransactionManager.TopTransaction;
            if (toptr == null)
            {
                throw new acExcep(Autodesk.AutoCAD.Runtime.ErrorStatus.NotTopTransaction);
            }
            ObjectIdCollection ids = new ObjectIdCollection();
            try
            {
                foreach (Entity entity in entities)
                {
                    ids.Add(owner.AppendEntity(entity));
                    toptr.AddNewlyCreatedDBObject(entity, true);
                }
            }
            catch
            {

                throw;
            }
            return ids;
        }

        public static ObjectIdCollection AddRange(this BlockTableRecord owner, params Entity[] entities)
        {
            return owner.Add(entities);
        }

        public static void DisposeAll<T>(this IEnumerable<T> disposables)
            where T : IDisposable
        {
            foreach (T disposable in disposables)
            {
                disposable.Dispose();
            }
        }

        public static void DisposeAll(this DBObjectCollection dbObjects)
        {
            foreach (object dbObj in dbObjects)
            {
                ((DBObject)dbObj).Dispose();
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T t in collection)
            {
                action(t);
            }
        }

        public static BlockTableRecord GetCurrentSpace(this Database db)
        {
            return db.CurrentSpaceId.GetObject<BlockTableRecord>();
        }

        public static BlockTableRecord GetCurrentSpace(this Database db, OpenMode mode)
        {
            return db.CurrentSpaceId.GetObject<BlockTableRecord>(mode);
        }

        public static BlockTableRecord GetModelSpace(this Database db)
        {
            return db.GetModelSpace(OpenMode.ForRead);
        }

        public static BlockTableRecord GetModelSpace(this Database db, OpenMode mode)
        {
            return SymbolUtilityServices.GetBlockModelSpaceId(db).GetObject<BlockTableRecord>(mode);
        }

        public static T GetObject<T>(this ObjectId id)
        where T : DBObject
        {
            return (T)id.GetObject(OpenMode.ForRead, false, false);
        }

        public static T GetObject<T>(this ObjectId id, OpenMode mode)
        where T : DBObject
        {
            return (T)id.GetObject(mode, false, false);
        }

        public static T GetObject<T>(this ObjectId id, OpenMode mode, bool openErased)
        where T : DBObject
        {
            return (T)id.GetObject(mode, openErased, false);
        }

        public static T GetObject<T>(this ObjectId id, OpenMode mode, bool openErased, bool onLockedLayers)
        where T : DBObject
        {
            return (T)id.GetObject(mode, openErased, onLockedLayers);
        }

        public static IEnumerable<T> GetObjects<T>(this IEnumerable<ObjectId> source)
        where T : DBObject
        {
            return source.GetObjects<T>(OpenMode.ForRead, false, false);
        }

        public static IEnumerable<T> GetObjects<T>(this IEnumerable<ObjectId> source, OpenMode mode)
        where T : DBObject
        {
            return source.GetObjects<T>(mode, false, false);
        }

        public static IEnumerable<T> GetObjects<T>(this IEnumerable<ObjectId> source, OpenMode mode, bool openErased)
        where T : DBObject
        {
            return source.GetObjects<T>(mode, openErased, false);
        }

        public static IEnumerable<T> GetObjects<T>(this IEnumerable<ObjectId> ids, OpenMode mode, bool openErased, bool forceOpenOnLockedLayers)
        where T : DBObject
        {
            if (ids.Any<ObjectId>())
            {
                TransactionManager transactionManager = ids.First<ObjectId>().Database.TransactionManager;
                RXClass @class = RXObject.GetClass(typeof(T));
                foreach (ObjectId id in ids)
                {
                    if (!(id.ObjectClass == @class) && !id.ObjectClass.IsDerivedFrom(@class))
                    {
                        continue;
                    }
                    yield return (T)transactionManager.GetObject(id, mode, openErased, forceOpenOnLockedLayers);
                }
                transactionManager = null;
                @class = null;
            }
        }

        public static IEnumerable<T> GetObjects<T>(this SymbolTable source)
        where T : SymbolTableRecord
        {
            return source.GetObjects<T>(OpenMode.ForRead, false);
        }

        public static IEnumerable<T> GetObjects<T>(this SymbolTable source, OpenMode mode)
        where T : SymbolTableRecord
        {
            return source.GetObjects<T>(mode, false);
        }

        public static IEnumerable<T> GetObjects<T>(this SymbolTable source, OpenMode mode, bool openErased)
        where T : SymbolTableRecord
        {
            TransactionManager transactionManager = source.Database.TransactionManager;
            foreach (ObjectId objectId in (openErased ? source.IncludingErased : source))
            {
                yield return (T)transactionManager.GetObject(objectId, mode, openErased, false);
            }
        }

        public static IEnumerable<T> GetObjects<T>(this BlockTableRecord source)
        where T : Entity
        {
            return source.GetObjects<T>(OpenMode.ForRead, false, false);
        }

        public static IEnumerable<T> GetObjects<T>(this BlockTableRecord source, OpenMode mode)
        where T : Entity
        {
            return source.GetObjects<T>(mode, false, false);
        }

        public static IEnumerable<T> GetObjects<T>(this BlockTableRecord source, OpenMode mode, bool openErased)
        where T : Entity
        {
            return source.GetObjects<T>(mode, openErased, false);
        }

        public static IEnumerable<T> GetObjects<T>(this BlockTableRecord btr, OpenMode mode, bool openErased, bool forceOpenOnLockedLayers)
        where T : Entity
        {
            BlockTableRecord blockTableRecords = (openErased ? btr.IncludingErased : btr);
            TransactionManager transactionManager = btr.Database.TransactionManager;
            if (typeof(T) != typeof(Entity))
            {
                RXClass @class = RXObject.GetClass(typeof(T));
                foreach (ObjectId objectId in blockTableRecords)
                {
                    if (!(objectId.ObjectClass == @class) && !objectId.ObjectClass.IsDerivedFrom(@class))
                    {
                        continue;
                    }
                    yield return (T)transactionManager.GetObject(objectId, mode, openErased, forceOpenOnLockedLayers);
                }
                @class = null;
            }
            else
            {
                foreach (ObjectId objectId1 in blockTableRecords)
                {
                    yield return (T)transactionManager.GetObject(objectId1, mode, openErased, forceOpenOnLockedLayers);
                }
            }
        }

        public static IEnumerable<AttributeReference> GetObjects(this AttributeCollection attribs)
        {
            return attribs.GetObjects(OpenMode.ForRead, false, false);
        }

        public static IEnumerable<AttributeReference> GetObjects(this AttributeCollection attribs, OpenMode mode)
        {
            return attribs.GetObjects(mode, false, false);
        }

        public static IEnumerable<AttributeReference> GetObjects(this AttributeCollection attribs, OpenMode mode, bool openErased)
        {
            return attribs.GetObjects(mode, openErased, false);
        }

        public static IEnumerable<AttributeReference> GetObjects(this AttributeCollection attribs, OpenMode mode, bool openErased, bool forceOpenOnLockedLayers)
        {
            foreach (ObjectId attrib in attribs)
            {
                yield return (AttributeReference)attrib.GetObject(mode, openErased, forceOpenOnLockedLayers);
            }
        }

        public static TextInfo GetTextInfo(this DBText text)
        {
            return new TextInfo(text.Position, text.AlignmentPoint, text.Justify != AttachmentPoint.BaseLeft);
        }

    }
}
