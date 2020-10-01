using System.Collections.Generic;
using System.Linq;

namespace OOP_Laba4 {
	static class StatisticOperation {
		public static double? Sum(this Set set) {
			double? sum = null;
			foreach (var item in set) {
				if (double.TryParse(item, out double result)) {
					if (!sum.HasValue)
						sum = 0.0;
					sum += result;
				}
			}
			return sum;
		}

		public static double? MinMaxDiff(this Set set) {
			double? diff = null;
			var list = new List<double>();
			foreach (var item in set) {
				if (double.TryParse(item, out double result))
					list.Add(result);
			}
			if (list.Count != 0) {
				diff = list.Max() - list.Min();
			}
			return diff;
		}

		public static int Count(this Set set) {
			var list = new List<string>();
			int counter = 0;
			foreach (var item in set) {
				if (list.Contains(item))
					continue;
				else {
					list.Add(item);
					counter++;
				}
			}
			return counter;
		}

		public static (List<string> Result, bool OnlyStr) Shortest(this Set set) {
			int length = int.MaxValue;
			var result = new List<string>();
			//var numbers = new List<double>();
			for (int i = 0; i < 2; i++) {
				foreach (var item in set) {
					if (i == 0) {
						if (double.TryParse(item, out double tRes)) {
							//numbers.Add(tRes);
							continue;
						}
					}
					if (item.Length == length)
						result.Add(item);
					else
					if (item.Length < length) {
						result.Clear();
						result.Add(item);
						length = item.Length;
					}
				}
				if (result.Count != 0 && i == 0)
					return (result, true);
			}
			return (result, false);
		}

		public static void Sort(this Set set, SortType type) {
			var nums = new List<double>();
			var strs = new List<string>();
			foreach (var item in set) {
				if (double.TryParse(item, out double result)) {
					if (!nums.Contains(result))
						nums.Add(result);
				}
				else
					if (!strs.Contains(item))
					strs.Add(item);
			}
			nums.Sort();
			strs.Sort();
			set.Data.Clear();
			switch (type) {
				case SortType.NumsStr:
					foreach (var item in nums)
						set.Add(item);
					set.Data.AddRange(strs);
					break;
				case SortType.StrsNums:
					set.Data.AddRange(strs);
					foreach (var item in nums)
						set.Add(item);
					break;
				default:
					break;
			}
		}
	}
}
