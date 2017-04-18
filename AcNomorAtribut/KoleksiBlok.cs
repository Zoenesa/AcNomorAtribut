using System.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AcNomorAtribut
{
    public class KoleksiBlok
    {
        private List<FileInfo> ListFileDwgs;

        private Database acDbs;

        private Document acDoc;

        /// <summary>
        /// Enum Type Pilihan Drawing
        /// </summary>
        public enum PilihanDrawing
        {
            CurrentDrawing = 0,
            PickFromDrawing = 1,
            ExternalDrawing = 2
        }

        public KoleksiBlok()
        {

        }

        public KoleksiBlok(PilihanDrawing mode)
        {
            switch (mode)
            {
                case PilihanDrawing.CurrentDrawing:
                    acDoc = AcAp.DocumentManager.MdiActiveDocument;
                    acDbs = acDoc.Database;
                    break;
                case PilihanDrawing.PickFromDrawing:
                    break;
                case PilihanDrawing.ExternalDrawing:
                    break;
                default:
                    break;
            }
        }

        public KoleksiBlok(PilihanDrawing mode, string[] FileNames)
        {
            switch (mode)
            {
                case PilihanDrawing.CurrentDrawing:
                    break;
                case PilihanDrawing.PickFromDrawing:
                    break;
                case PilihanDrawing.ExternalDrawing:
                    ListFileDwgs = new List<FileInfo>();
                    foreach (string file in FileNames)
                    {
                        ListFileDwgs.Add(new FileInfo(file));
                    }   
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Get Koleksi Blok Table ID
        /// </summary>
        public Dictionary<string, ObjectId> KoleksiBlokTableId = new Dictionary<string, ObjectId>();

        /// <summary>
        /// Get Koleksi Block Has Reference Ids
        /// </summary>
        public Dictionary<string, ObjectId> KoleksiBlockReferenceIds = new Dictionary<string, ObjectId>();

        /// <summary>
        /// Get Koleksi All Blok Attribut From Block Reference Ids
        /// </summary>
        public Dictionary<string, ObjectId> KoleksiAtributBlokRefs = new Dictionary<string, ObjectId>();

        /// <summary>
        /// File Drawings
        /// </summary>
        public List<FileInfo> FileDrawings
        {
            get
            {
                return ListFileDwgs;
            }
        }

        /// <summary>
        /// Get Block Attributes From Drawing
        /// </summary>
        /// <param name="mode">Select All From Current Drawing, Pick From Current Drawing, Select All From External Drawing</param>
        public void GetBlokByDrawing(PilihanDrawing mode)
        {
            switch (mode)
            {
                case PilihanDrawing.CurrentDrawing:
                    acDoc = AcAp.DocumentManager.MdiActiveDocument;
                    acDbs = acDoc.Database;
                    break;
                case PilihanDrawing.PickFromDrawing:
                  
                    break;
                case PilihanDrawing.ExternalDrawing:
                    for (int i = 0; i < (FileDrawings.Count); i++)
                    {
                        acDbs = new Database(false, true);
                        string file = FileDrawings[i].FullName;
                        try
                        {
                            acDbs.ReadDwgFile(file, FileShare.Read, false, "");
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception AcEx)
                        {
                            throw new Autodesk.AutoCAD.Runtime.Exception(ErrorStatus.CantOpenFile, "Error Loading File" + "\n" + AcEx.Message);
                        }
                        using (Transaction tr = acDbs.TransactionManager.StartTransaction())
                        {
                            BlockTable bt = (BlockTable)acDbs.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                            foreach (ObjectId tbId in bt)
                            {
                                BlockTableRecord btr = (BlockTableRecord)tbId.GetObject(OpenMode.ForRead);
                                if (btr.IsAnonymous)
                                {
                                    continue;
                                }
                                //KoleksiBlokTableId = new Dictionary<string, ObjectId>();

                                if (btr.GetBlockReferenceIds(true, false).Count > 0 )
                                {
                                    if (btr.HasAttributeDefinitions)
                                    {
                                    KoleksiBlokTableId.Add(btr.Name, tbId);
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        
        public Dictionary<string, ObjectId> GetDataBlokAttributes(PilihanDrawing mode)
        {
            Dictionary<string, ObjectId> newList = new Dictionary<string, ObjectId>();
            switch (mode)
            {
                case PilihanDrawing.CurrentDrawing:
                    break;
                case PilihanDrawing.PickFromDrawing:
                    break;
                case PilihanDrawing.ExternalDrawing:
                    break;
                default:
                    break;
            }

            return newList;
        }

        /// <summary>
        /// Interact With Progress Meter CAD Aplication
        /// </summary>
        /// <param name="display">Display String On Progress Meter</param>
        /// <param name="limit">Set Limit / Max. Value</param>
        public void AcProgress(string display, int limit)
        {
            ProgressMeter pm = new ProgressMeter();
            pm.Start(display);
            pm.SetLimit(limit);
            for (int i = 0; i < limit; i++)
            {
                pm.MeterProgress();
                System.Threading.Thread.Sleep(10);
                System.Windows.Forms.Application.DoEvents();
            }
            pm.Stop();
        }

        /// <summary>
        /// Interact With Progress Meter CAD Application
        /// </summary>
        /// <param name="Display">Display String On Progress Meter</param>
        /// <param name="limit">Set Limit / Max. Value</param>
        /// <param name="usedelay">usedelay if true with delay</param>
        /// <param name="delay">delay each loop per milisecond</param>
        public void AcProgress(string Display, int limit, bool usedelay, int delay)
        {
            ProgressMeter pm = new ProgressMeter();
            pm.Start(Display);
            pm.SetLimit(limit);
            for (int i = 0; i < limit; i++)
            {
                if (usedelay)
                {
                    System.Threading.Thread.Sleep(delay);
                    pm.MeterProgress();
                }
                System.Windows.Forms.Application.DoEvents();
                pm.MeterProgress();
            }
            pm.Stop();
        }

    }
}
