namespace ESTIME.BusinessLibrary.Object
{
    public class CodeSetType
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }

        public CodeSetType(DAL.EstimeEntity.CodeSetType cstype)
        {
            ID = cstype.Id;
            Value = cstype.Code;
            NameEnglish = cstype.NameEnglish;
            NameFrench = cstype.NameFrench;
        }
    }
}
