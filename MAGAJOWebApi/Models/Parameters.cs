using System.Data;

namespace MAGAJOWebApi.Models
{
    public class Parameters
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public int Size { get; set; }
        public SqlDbType Type { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}
