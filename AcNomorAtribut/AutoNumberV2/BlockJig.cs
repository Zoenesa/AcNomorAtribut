using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AcBlockAtributeIncrement
{
    internal class BlockJig : EntityJig
    {
        private Point3d _pos;

        private List<TextInfo> _attInfos;

        private BlockReference _br;

        private Database _db;

        private Transaction _tr;

        private int _cnt;

        private bool _isFrench;

        public BlockJig(BlockReference br, int cnt, List<TextInfo> attInfos) : base(br)
        {
            this._br = br;
            this._db = this._br.Database;
            this._tr = this._db.TransactionManager.TopTransaction;
            this._attInfos = attInfos;
            this._pos = br.Position;
            this._cnt = cnt;
            this._isFrench = Application.GetSystemVariable("LOCALROOTPREFIX").ToString().EndsWith("fra\\");
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions jigPromptPointOption = new JigPromptPointOptions();
            jigPromptPointOption.SetMessageAndKeywords((this._isFrench ? "\nSpécifiez le point d'insertion [annUler]: " : "\nSpecify insertion point [Undo]"), (this._isFrench ? "annUler" : "Undo"));
            jigPromptPointOption.AppendKeywordsToMessage = this._cnt > 0;
            jigPromptPointOption.UserInputControls = UserInputControls.Accept3dCoordinates | UserInputControls.NullResponseAccepted;
            PromptPointResult promptPointResult = prompts.AcquirePoint(jigPromptPointOption);
            if (this._pos.DistanceTo(promptPointResult.Value) < Tolerance.Global.EqualPoint)
            {
                return SamplerStatus.NoChange;
            }
            this._pos = promptPointResult.Value;
            return SamplerStatus.OK;
        }

        protected override bool Update()
        {
            int num1 = 0;
            this._br.Position = this._pos;
            this._br.AttributeCollection.GetObjects(OpenMode.ForWrite).ForEach<AttributeReference>((AttributeReference att) => {
                List<TextInfo> u003cu003e4_this = this._attInfos;
                int num = num1;
                num1 = num + 1;
                TextInfo item = u003cu003e4_this[num];
                Point3d position = item.Position;
                att.Position = position.TransformBy(this._br.BlockTransform);
                if (item.IsAligned)
                {
                    position = item.Alignment;
                    att.AlignmentPoint = position.TransformBy(this._br.BlockTransform);
                    att.AdjustAlignment(this._br.Database);
                }
                if (att.IsMTextAttribute)
                {
                    att.UpdateMTextAttribute();
                }
            });
            return true;
        }
    }
}