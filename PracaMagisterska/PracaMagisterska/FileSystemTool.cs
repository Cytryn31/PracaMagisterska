using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaMagisterska
{
    public class FileSystemTool
    {
        public Image ReadFromFile(string path)
        {
            return Image.FromFile(path);
        }
    }
}
