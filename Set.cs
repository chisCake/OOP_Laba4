﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

#pragma warning disable CS0660 // Тип определяет оператор == или оператор !=, но не переопределяет Object.Equals(object o)
#pragma warning disable CS0661 // Тип определяет оператор == или оператор !=, но не переопределяет Object.GetHashCode()
namespace OOP_Laba4 {
	/*
		Класс - множество Set
		Дополнительно перегрузить следующие операции:
			+ - добавить элемент в множество (типа set+item)
			- - удалить элемент из множества (типа set-item)
			* - проверка на подмножество
			!= - проверка множеств на неравенство
			% - пересечение множеств.
		Методы расширения:
			1) Поиск самого короткого слова
			2) Упорядочивание множества
	*/
	class Set {
		// Данные
		public List<string> Data { get; private set; }
		public Owner Owner { get; private set; } = new Owner();
		public DateTime Date { get; private set; } = DateTime.Now;

		// Конструкторы
		public Set(Owner owner = null) {
			Data = new List<string>();
			Owner = owner != null ? new Owner(owner) : new Owner();
		}

		public Set(string[] data, Owner owner = null) {
			Data = new List<string>(data);
			Owner = owner != null ? new Owner(owner) : new Owner();
		}

		public Set(params dynamic[] data) {
			var list = new List<string>();
			foreach (var item in data) {
				list.Add(item.ToString());
			}
			Data = list;
			Owner = new Owner();
		}

		public Set(Owner owner = null, params dynamic[] data) {
			var list = new List<string>();
			foreach (var item in data) {
				list.Add(item.ToString());
			}
			Data = list;
			Owner = owner != null ? new Owner(owner) : new Owner();
		}

		public Set(Set set, string item, MathOperation type) {
			Data = new List<string>(set.Data);
			Owner = new Owner(set.Owner);
			switch (type) {
				case MathOperation.plus:
					Add(item);
					break;
				case MathOperation.minus:
					if (Data.Contains(item)) {
						Data.RemoveAll(elem => elem == item);
					}
					break;
				default:
					break;
			}
		}

		// Индексатор
		public string this[int index] {
			get => Data[index];
			set => Data[index] = value;
		}

		public IEnumerator<string> GetEnumerator(){
			return Data.GetEnumerator();
		}

		// Методы
		public void Add(string item) {
			Data.Add(item);
		}

		public void Add(dynamic item) {
			Data.Add(item.ToString());
		}

		public bool Remove(string item) {
			if (Data.Contains(item)) {
				Data.RemoveAll(el => el == item);
				return true;
			}
			return false;
		}

		public bool IsSetOf(Set set) {
			if (Data.All(item => set.Data.Contains(item))) {
				return true;
			}
			return false;
		}

		public bool IsEqual(Set set) {
			return (
				Data.All(elem1 => set.Data.Any(elem2 => elem2 == elem1)) &&
				set.Data.All(elem1 => Data.Any(elem2 => elem2 == elem1))
				);
		}

		// Перегружаемые операторы

		// Добавление
		public static Set operator +(Set set, string str) {
			return new Set(set, str, MathOperation.plus);
		}

		// Удаление
		public static Set operator -(Set set, string str) {
			return new Set(set, str, MathOperation.minus);
		}

		// Проверка на подмножество
		public static bool operator *(Set set1, Set set2) {
			return set1.IsSetOf(set2);
		}

		// Проверка на неравенство
		public static bool operator !=(Set set1, Set set2) {
			return !set1.IsEqual(set2);
		}

		// Проверка на равенство
		public static bool operator ==(Set set1, Set set2) {
			return set1.IsEqual(set2);
		}

		// Проверка на пересечение
		public static bool operator %(Set set1, Set set2) {
			return set1.Data.Any(elem1 => set2.Data.Any(elem2 => elem1 == elem2));
		}
	}
}
#pragma warning restore CS0661 // Тип определяет оператор == или оператор !=, но не переопределяет Object.GetHashCode()
#pragma warning restore CS0660 // Тип определяет оператор == или оператор !=, но не переопределяет Object.Equals(object o)