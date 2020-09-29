using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;

using Microsoft.Win32.SafeHandles;

namespace OOP_Laba4 {
	class Program {
		static List<Set> List { get; set; }
		delegate void Delegate();
		static readonly List<Delegate> Tasks = new List<Delegate> {
			new Delegate(Plus),
			new Delegate(Minus),
			new Delegate(Subset),
			new Delegate(Inequality),
			new Delegate(Intersection),
			new Delegate(Info),
			new Delegate(Sum),
			new Delegate(MinMaxDiff),
			new Delegate(Counting),
			new Delegate(ShortestStr),
			new Delegate(Sort),
		};

		static void Main() {
			List = new List<Set> {
			new Set(new Set.Owner(110, "Set1", "ЗАО СЕТ"), 1, 2, 3, 4, 5, 6, 7, 8, 9, 10),
			new Set(new Set.Owner(123, "Set2", "ОАО СЕТ"), "один", "два", "три"),
			new Set(new Set.Owner("Set3"), 2, 4, 6),
			new Set(new Set.Owner("Set4"), "один", "два", "три"),
			new Set(new Set.Owner("Set5"), "один", 2, 5, -100.255, "два", 450, 2, 5)
			};


			while (true) {
				Console.Clear();
				Console.WriteLine("Текущие множества:");
				for (int i = 0; i < List.Count; i++) {
					Console.Write($"{i + 1}) {List[i].OwnerData.Name}: ");
					foreach (var item in List[i].Data)
						Console.Write(item + " ");
					Console.WriteLine();
				}

				Console.Write(
					"\n1) Операция сложения" +
					"\n2) Операция вычитания" +
					"\n3) Проверка на подмножество" +
					"\n4) Проверка на неравенство" +
					"\n5) Проверка на пересечение" +
					"\n6) Вывод информации о множестве" +
					"\n7) Сумма элементов" +
					"\n8) Разница между минимальным и максимальным элементом" +
					"\n9) Подсчёт кол-ва элементов" +
					"\n10) Поиск самого короткого слова" +
					"\n11) Упорядочить множество" +
					"\n0) Выход" +
					"\nВыберите действие: "
					);

				if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= Tasks.Count) {
					if (choice == 0) {
						Console.WriteLine("Выход...");
						return;
					}
					Console.WriteLine();
					Tasks[choice - 1]();
				}
				else
					Console.WriteLine("Нет такого варианта");

				Console.WriteLine("\n\nPress any key");
				Console.ReadKey();
			}
		}

		#region Services

		// Выбор множеств
		static Set GetSet() {
			int set;
			do {
				Console.Write("\nВведите номер множества: ");
				if (int.TryParse(Console.ReadLine(), out set)) {
					if (set == 0)
						return null;
					if (set > 0 && set <= List.Count)
						break;
					else
						Console.WriteLine("Нет такого множества");
				}
			} while (true);
			return List[set - 1];
		}

		static (Set, Set) GetSets() {
			int set1, set2;
			do {
				Console.Write("Введите номер 1го множества: ");
				if (int.TryParse(Console.ReadLine(), out set1)) {
					if (set1 == 0)
						return (null, null);
					if (set1 > 0 && set1 <= List.Count)
						break;
					else
						Console.WriteLine("Нет такого множества");
				}
			} while (true);
			do {
				Console.Write("Введите номер 2го множества: ");
				if (int.TryParse(Console.ReadLine(), out set2)) {
					if (set2 == 0)
						return (null, null);
					if (set2 > 0 && set2 <= List.Count)
						break;
					else
						Console.WriteLine("Нет такого множества");
				}
			} while (true);
			return (List[set1 - 1], List[set2 - 1]);
		}

		//Ввод имени множества
		static void SetName(ref Set set) {
			Console.Write("Нажмите \"1\" чтобы дать уникальное название новому множеству: ");
			if (Console.ReadKey().KeyChar == '1') {
				Console.Write("\nВведите название множества: ");
				set.OwnerData.Name = Console.ReadLine();
			}
			else
				set.OwnerData.Name = $"Set{List.Count + 1}";
		}

		#endregion

		static void Plus() {
			Set set = GetSet();
			if (Equals(set, null))
				return;
			Console.Write("Введите с чем сложить это множество: ");
			string item = Console.ReadLine();
			var result = set + item;
			SetName(ref result);
			List.Add(result);
		}

		static void Minus() {
			Set set = GetSet();
			if (Equals(set, null))
				return;
			Console.Write("Введите что вычесть из этого множества: ");
			string item = Console.ReadLine();
			var result = set - item;
			SetName(ref result);
			List.Add(result);
		}

		static void Subset() {
			Console.WriteLine("1 - подмножество, 2 - множество");
			var sets = GetSets();
			if (Equals(sets.Item1, null) || Equals(sets.Item2, null))
				return;
			if (sets.Item1 * sets.Item2)
				Console.WriteLine($"{sets.Item1.OwnerData.Name} является подмножеством {sets.Item2.OwnerData.Name}");
			else
				Console.WriteLine($"{sets.Item1.OwnerData.Name} не является подмножеством {sets.Item2.OwnerData.Name}");
		}

		static void Inequality() {
			var sets = GetSets();
			if (Equals(sets.Item1, null) || Equals(sets.Item2, null))
				return;
			if (sets.Item1 != sets.Item2)
				Console.WriteLine($"{sets.Item1.OwnerData.Name} не равно {sets.Item2.OwnerData.Name}");
			else
				Console.WriteLine($"{sets.Item1.OwnerData.Name} равно {sets.Item2.OwnerData.Name}");
		}

		static void Intersection() {
			var sets = GetSets();
			if (Equals(sets.Item1, null) || Equals(sets.Item2, null))
				return;
			if (sets.Item1 % sets.Item2)
				Console.WriteLine($"{sets.Item1.OwnerData.Name} пересекается с {sets.Item2.OwnerData.Name}");
			else
				Console.WriteLine($"{sets.Item1.OwnerData.Name} не пересекается с {sets.Item2.OwnerData.Name}");
		}

		static void Info() {
			var set = GetSet();
			Console.WriteLine("Элементы в множестве:");
			foreach (var item in set)
				Console.Write(item + " ");
			Console.WriteLine(
				$"\nID: {set.OwnerData.Id}" +
				$"\nИмя: {set.OwnerData.Name}" +
				$"\nОрганизация: {set.OwnerData.Organization}" +
				$"\nДата создания: {set.Date}"
				);
		}

		static void Sum() {
			var set = GetSet();
			var result = StatisticOperation.Sum(set);
			if (result.HasValue)
				Console.WriteLine($"Сумма всех чисел множества: {result.Value}");
			else
				Console.WriteLine("Множество не содержит числовых значений");
		}

		static void MinMaxDiff() {
			var set = GetSet();
			var result = StatisticOperation.MinMaxDiff(set);
			if (result.HasValue)
				Console.WriteLine($"Разница между самым минимальным и максимальным значением: {result.Value}");
			else
				Console.WriteLine("Множество не содержит числовых значений");
		}

		static void Counting() {
			var set = GetSet();
			Console.WriteLine($"Кол-во элементов во множестве: {StatisticOperation.Count(set)}");
		}

		static void ShortestStr() {
			var set = GetSet();
			var result = StatisticOperation.Shortest(set);
			if (result.OnlyStr)
				if (result.Result.Count == 1)
					Console.WriteLine($"Самое короткое слово: {result.Result[0]}");
				else {
					Console.WriteLine("Самые короткие слова:");
					foreach (var item in result.Result)
						Console.Write(item + " ");
				}
			else
				if (result.Result.Count == 1)
				Console.WriteLine($"Слов во множестве нет, самое малозначное число: {result.Result[0]}");
			else {
				Console.WriteLine("Слов во множестве нет, самые малозначное числа:");
				foreach (var item in result.Result)
					Console.Write(item + " ");
			}
		}

		static void Sort() {
			var set = GetSet();
			Console.Write(
				"1 - вначале числа, затем слова" +
				"\n2 - вначале слова, затем числа" +
				"\nВыберите тип сортировки: "
				);
			string choice = Console.ReadLine();
			if (choice != "1" && choice != "2")
				return;
			Console.WriteLine("До сортировки: ");
			foreach (var item in set) 
				Console.Write(item + " ");
			StatisticOperation.Sort(set, (SortType)(Convert.ToInt32(choice) - 1));
			Console.WriteLine("\nПосле сортировки: ");
			foreach (var item in set) 
				Console.Write(item + " ");
		}
	}
}
