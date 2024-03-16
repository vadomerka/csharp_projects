using System.Security.Cryptography;
using System.Text;

namespace DataProcessing
{
    public interface ICSVItem
    {
        public string ToJSON() { return ""; }
    }

    public class Plant : ICSVItem
    {

        private int _id;
        private string _name = "";
        private string _latinName = "";
        private string _photo = "";
        private string _landscapingZone = "";
        private string _prosperityPeriod = "";
        private string _description = "";
        private string _locationPlace = "";
        private string _viewForm = "";
        private int _global_id;

        public Plant() { }

        public Plant(int id, string name, string latinName, string photo,
                     string landscapingZone, string prosperityPeriod,
                     string description, string locationPlace, string viewForm,
                     int global_id)
        {
            _id = id;
            _name = name;
            _latinName = latinName;
            _photo = photo;
            _landscapingZone = landscapingZone;
            _prosperityPeriod = prosperityPeriod;
            _description = description;
            _locationPlace = locationPlace;
            _viewForm = viewForm;
            _global_id = global_id;
        }

        public Plant(string? wkt)
        {
            if (wkt == null) throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(wkt)) throw new ArgumentNullException();
            wkt = wkt.Trim('"');
            var line = wkt.Split("\";\"", StringSplitOptions.RemoveEmptyEntries);
            if (line.Length != 10) throw new FormatException();
            try
            {
                _id = int.Parse(line[0]);
                _name = line[1];
                _latinName = line[2];
                _photo = line[3];
                _landscapingZone = line[4];
                _prosperityPeriod = line[5];
                _description = line[6];
                _locationPlace = line[7];
                _viewForm = line[8];
                _global_id = int.Parse(line[9]);
            }
            catch 
            {
                throw new FormatException();
            }
        }

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public string LatinName { get { return _latinName; } }
        public string Photo { get { return _photo; } }
        public string LandscapingZone { get { return _landscapingZone; } }
        public string ProsperityPeriod { get { return _prosperityPeriod; } }
        public string Description { get { return _description; } }
        public string LocationPlace { get { return _locationPlace; } }
        public string ViewForm { get { return _viewForm; } }
        public int GlobalId { get { return _global_id; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\"{_id}\";");
            sb.Append($"\"{_name}\";");
            sb.Append($"\"{_latinName}\";");
            sb.Append($"\"{_photo}\";");
            sb.Append($"\"{_landscapingZone}\";");
            sb.Append($"\"{_prosperityPeriod}\";");
            sb.Append($"\"{_description}\";");
            sb.Append($"\"{_locationPlace}\";");
            sb.Append($"\"{_viewForm}\";");
            sb.Append($"\"{_global_id}\";");
            return sb.ToString();
        }
    }

    public class Header : ICSVItem
    {
        private string _id = "";
        private string _name = "";
        private string _latinName = "";
        private string _photo = "";
        private string _landscapingZone = "";
        private string _prosperityPeriod = "";
        private string _description = "";
        private string _locationPlace = "";
        private string _viewForm = "";
        private string _global_id = "";
        
        public Header() { }

        public Header(string id, string name, string latinName, string photo, string landscapingZone, 
            string prosperityPeriod, string description, string locationPlace, 
            string viewForm, string global_id)
        {
            _id = id;
            _latinName = latinName;
            _photo = photo;
            _landscapingZone = landscapingZone;
            _prosperityPeriod = prosperityPeriod;
            _description = description;
            _locationPlace = locationPlace;
            _viewForm = viewForm;
            _global_id = global_id;
        }

        public Header(string? wkt)
        {
            if (wkt == null) throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(wkt)) throw new ArgumentNullException();
            var line = wkt.Split(";", StringSplitOptions.RemoveEmptyEntries);
            if (line.Length != 10) throw new FormatException();
            try
            {
                for (int i = 0; i < line.Length; i++)
                {
                    line[i] = line[i].Trim('"').Replace("\n", "");
                }
                _id = line[0];
                _name = line[1];
                _latinName = line[2];
                _photo = line[3];
                _landscapingZone = line[4];
                _prosperityPeriod = line[5];
                _description = line[6];
                _locationPlace = line[7];
                _viewForm = line[8];
                _global_id = line[9];
            }
            catch
            {
                throw new FormatException();
            }
        }

        public string Id { get { return _id; } }
        public string Name { get { return _name; } }
        public string LatinName { get { return _latinName; } }
        public string Photo { get { return _photo; } }
        public string LandscapingZone { get { return _landscapingZone; } }
        public string ProsperityPeriod { get { return _prosperityPeriod; } }
        public string Description { get { return _description; } }
        public string LocationPlace { get { return _locationPlace; } }
        public string ViewForm { get { return _viewForm; } }
        public string GlobalId { get { return _global_id; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\"{_id}\";");
            sb.Append($"\"{_name}\";");
            sb.Append($"\"{_latinName}\";");
            sb.Append($"\"{_photo}\";");
            sb.Append($"\"{_landscapingZone}\";");
            sb.Append($"\"{_prosperityPeriod}\";");
            sb.Append($"\"{_description}\";");
            sb.Append($"\"{_locationPlace}\";");
            sb.Append($"\"{_viewForm}\";");
            sb.Append($"\"{_global_id}\";");
            return sb.ToString();
        }
    }
}