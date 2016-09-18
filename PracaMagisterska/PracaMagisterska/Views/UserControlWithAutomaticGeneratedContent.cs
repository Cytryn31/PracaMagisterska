using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PracaMagisterska.Views
{
	public partial class UserControlWithAutomaticGeneratedContent : UserControl
	{
		public UserControlWithAutomaticGeneratedContent()
		{
			InitializeComponent();
		}

		public void RefreshVals()
		{
			foreach (var control in Controls)
			{
				if (control is TextBox)
				{
					_values[(control as TextBox).Name] = (control as TextBox).Text;
				}

				if (control is ComboBox)
				{
					_values[(control as ComboBox).Name] = (control as ComboBox).SelectedItem.ToString();
				}
			}
			xPos = 5;
			yPos = 15;
			Controls.Clear();
		}

		public void FiillVals()
		{
			foreach (var algorithm in Algorithms.Instance.Items)
			{
				foreach (var algorithmParameter in algorithm.Parameters)
				{
					if (!_values.ContainsKey(algorithmParameter.Name))
						CreateNewOrUpdateExisting(_values, algorithmParameter.Name, algorithmParameter.Value);

				}
			}
		}

		public void AddField(string label, ParameterType type, string algoName, string algoVal)
		{
			var label1 = new Label
			{
				AutoSize = true,
				Location = new Point(xPos, yPos),
				Name = label,
				Text = label
			};
			Controls.Add(label1);
			if (type != ParameterType.Enum)
			{
				if (!_values.ContainsKey(label))
					CreateNewOrUpdateExisting(_values, label, algoVal);
				var textBox1 = new TextBox
				{
					Location = new Point(xPos, yPos + 15),
					Name = label,
					Text = _values[label]
				};
				CreateNewOrUpdateExisting(_values, label, textBox1.Text);
				Controls.Add(textBox1);
			}

			if (type == ParameterType.Enum)
			{
				CreateNewOrUpdateExisting(_values, label, algoVal);
				var textBox1 = new TextBox
				{
					Location = new Point(xPos, yPos + 15),
					Name = label,
					Text = _values[label],
					ReadOnly = true,
					Size = new Size(150, 25),
				};
				CreateNewOrUpdateExisting(_values, label, textBox1.Text);
				Controls.Add(textBox1);
			}

			xPos += 250;
			if (xPos > 200)
			{
				xPos = 5;
				yPos += 35;
			}
		}

		public static void CreateNewOrUpdateExisting<TKey, TValue>(IDictionary<TKey, TValue> map, TKey key, TValue value)
		{
			if (map.ContainsKey(key))
			{
				map[key] = value;
			}
			else
			{
				map.Add(key, value);
			}
		}

		public List<Algorithm> GetAlgorithms(List<Algorithm> list)
		{
			foreach (var algorithm in list)
			{
				foreach (var para in algorithm.Parameters)
				{
					para.Value = _values[para.Name];
				}
			}
			return list;
		}

		private readonly Dictionary<string, string> _values = new Dictionary<string, string>();
		private int xPos = 5;
		private int yPos = 15;
	}
}
