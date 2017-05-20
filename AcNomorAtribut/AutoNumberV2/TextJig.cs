using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;

namespace AcBlockAtributeIncrement
{
    internal class TextJig : EntityJig
    {
        private Database _db;

        private Point3d _pos;

        private DBText _text;

        private int _cnt;

        private bool _left;

        private bool _isFrench;

        public TextJig(DBText text, int cnt, bool left, Database db) : base(text)
        {
            this._db = db;
            this._text = text;
            this._left = left;
            this._cnt = cnt;
            this._pos = (left ? text.Position : text.AlignmentPoint);
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
            if (!this._left)
            {
                this._text.AlignmentPoint = this._pos;
                this._text.AdjustAlignment(this._db);
            }
            else
            {
                this._text.Position = this._pos;
            }
            return true;
        }
    }
}