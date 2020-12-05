using MCRP.MSF.Service.Common;

namespace Template.ORM.Models
{
    public abstract class BaseModel : DOEntityBase
    {
        public string ChanPinID { get; set; }
    }
}
