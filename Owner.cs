using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Laba4 {
	class Owner {
		public string Id { get; private set; }
		public string Name { get; set; }
		public string Organization { get; set; }

		public Owner() {
			Id = "Не указано";
			Name = "Не указано";
			Organization = "Не указана";
		}

		public Owner(string name) {
			Id = "0";
			Name = name;
			Organization = "Не указана";
		}

		public Owner(string id = "Не указано",
					string name = "Не указано",
					string organization = "Не указана") {

			Id = id;
			Name = name;
			Organization = organization;
		}

		public Owner(
			int id = 0,
			string name = "Не указано",
			string organization = "Не указана") {

			if (id == 0)
				Id = "Не указано";
			else
				Id = id.ToString();
			Name = name;
			Organization = organization;
		}

		public Owner(Owner owner) {
			Id = owner.Id;
			Name = owner.Name;
			Organization = owner.Organization;
		}
	}
}
