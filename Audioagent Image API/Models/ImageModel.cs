using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Audioagent_Image_API.Models
{
    public class ImageModel
    {
        private Image _image;
        private readonly Dictionary<string, string> _metaData = new Dictionary<string, string>();

        public string FileName
        {
            get
            {
                var title = "";
                if (_image != null)
                {
                    if (_image.PropertyItems.Any(x => x.Id == 0x0320))
                    {
                        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                        title = encoding.GetString(_image.PropertyItems.First(x => x.Id == 0x0320).Value);
                    }
                }
                return title;
            }
        }
        public Dictionary<string, string> Metadata
        {
            get { return _metaData; }
        }

        private Dictionary<string, string> ProcessPropertyItems()
        {



            var dict = new Dictionary<string, string>();
            if (_image != null)
            {
                foreach (var item in _image.PropertyItems)
                {
                    dict[item.Id.ToString()] = GetPropertyData(item);
                }
            }
            return dict;
        }

        private static string GetPropertyData(PropertyItem properyItem)
        {
            using (ExifReader reader = new ExifReader(@"C:\temp\testImage.jpg"))
            {

            }
            System.Drawing.Imaging.PropertyItem.
            ImagePropertyDataTypes type = (ImagePropertyDataTypes)properyItem.Type;
            string result = "";
            string data = null;
            int num_items, item_size;
            switch (type)
            {
                case ImagePropertyDataTypes.ByteArray:
                case ImagePropertyDataTypes.UByteArray:
                    data =
                        BitConverter.ToString(properyItem.Value);
                    break;

                case ImagePropertyDataTypes.String:
                    data = Encoding.UTF8.GetString(
                        properyItem.Value, 0, properyItem.Len - 1);
                    break;

                case ImagePropertyDataTypes.UShortArray:
                    result = "";
                    item_size = 2;
                    num_items = properyItem.Len / item_size;
                    for (int i = 0; i < num_items; i++)
                    {
                        ushort value = BitConverter.ToUInt16(
                            properyItem.Value, i * item_size);
                        result += ", " + value.ToString();
                    }
                    if (result.Length > 0) result = result.Substring(2);
                    data = "[" + result + "]";
                    break;

                case ImagePropertyDataTypes.ULongArray:
                    result = "";
                    item_size = 4;
                    num_items = properyItem.Len / item_size;
                    for (int i = 0; i < num_items; i++)
                    {
                        uint value = BitConverter.ToUInt32(
                            properyItem.Value, i * item_size);
                        result += ", " + value.ToString();
                    }
                    if (result.Length > 0) result = result.Substring(2);
                    data = "[" + result + "]";
                    break;

                case ImagePropertyDataTypes.ULongFractionArray:
                    result = "";
                    item_size = 8;
                    num_items = properyItem.Len / item_size;
                    for (int i = 0; i < num_items; i++)
                    {
                        uint numerator = BitConverter.ToUInt32(
                            properyItem.Value, i * item_size);
                        uint denominator = BitConverter.ToUInt32(
                            properyItem.Value,
                            i * item_size + item_size / 2);
                        result += ", " + numerator.ToString() +
                            "/" + denominator.ToString();
                    }
                    if (result.Length > 0) result = result.Substring(2);
                    data = "[" + result + "]";
                    break;

                case ImagePropertyDataTypes.LongArray:
                    result = "";
                    item_size = 4;
                    num_items = properyItem.Len / item_size;
                    for (int i = 0; i < num_items; i++)
                    {
                        int value = BitConverter.ToInt32(
                            properyItem.Value, i * item_size);
                        result += ", " + value.ToString();
                    }
                    if (result.Length > 0) result = result.Substring(2);
                    data = "[" + result + "]";
                    break;

                case ImagePropertyDataTypes.LongFractionArray:
                    result = "";
                    item_size = 8;
                    num_items = properyItem.Len / item_size;
                    for (int i = 0; i < num_items; i++)
                    {
                        int numerator = BitConverter.ToInt32(
                            properyItem.Value, i * item_size);
                        int denominator = BitConverter.ToInt32(
                            properyItem.Value,
                            i * item_size + item_size / 2);
                        result += ", " + numerator.ToString() +
                            "/" + denominator.ToString();
                    }
                    if (result.Length > 0) result = result.Substring(2);
                    data = "[" + result + "]";
                    break;
            }
            return data;
        }

        public enum ImagePropertyDataTypes : short
        {
            ByteArray = 1,
            String = 2,
            UShortArray = 3,
            ULongArray = 4,
            ULongFractionArray = 5,
            UByteArray = 6,
            LongArray = 7,
            LongFractionArray = 10,
        }

        public ImageModel()
        {
        }
        public ImageModel(Image image)
        {
            _image = image;
            _metaData = ProcessPropertyItems();
        }
    }
}