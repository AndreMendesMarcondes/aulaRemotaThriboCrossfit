namespace AulaRemotaThriboCrossfit.Models
{
    public class BaseDomain
    {
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
