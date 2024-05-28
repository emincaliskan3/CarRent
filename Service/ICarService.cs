using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Service
{
	public interface ICarService
	{
		IEnumerable<Car> GetAllCars();


		Car GetCarById(int id);


		void AddCar(Car car);


		void UpdateCar(Car car);


		void DeleteCar(int id);
	}
}
