using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchDoctor {
    #region Doctor Class
    public class Doctor {
        // I assume wherever the user input for Area, it will automatically related to a zipcode
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int Zipcode { get; set; }
        public double ReviewScore { get; set; }
        
        public List<Doctor> Create() {
            List<Doctor> list = new List<Doctor>() {
                new Doctor() { Name="Steeve", Specialty="Allergy", Zipcode= 20166 , ReviewScore=4.5 },
                new Doctor() { Name="Nithy", Specialty="Allergy", Zipcode= 10011, ReviewScore=3.5},
                new Doctor() { Name="Micheal", Specialty="Allergy", Zipcode=95035, ReviewScore=4.8 },
                new Doctor() { Name="Tracy", Specialty="Allergy", Zipcode=20000, ReviewScore=4.2 },
                new Doctor() { Name="Barry", Specialty="Nutritionist", Zipcode=52342, ReviewScore=4.8},
                new Doctor() { Name="Jeff", Specialty="Nutritionist", Zipcode = 43101, ReviewScore = 3.2},
                new Doctor() { Name="Yuma", Specialty="Nutritionist", Zipcode= 67854, ReviewScore= 4.0},
                new Doctor() { Name="Alice", Specialty="Emergency", Zipcode= 73419, ReviewScore= 4.6},
                new Doctor() { Name="Jane", Specialty="Emergency", Zipcode=88888, ReviewScore=3.7},
                new Doctor() { Name="Tina", Specialty="Emergency", Zipcode=66666, ReviewScore=3.9}
            };
            return list;
        }
    }
    #endregion

    #region Filter result
    //If i have more specific data, i can create more accurate Model for calculating and determing the weights 
    public class Search {
        /**********Function for calculating similarities**********
        ***********  Filter by Specialty                **********
        ***********  Priority Order is based on below   **********
        ***********  Diff(zipcodes)/10 + ReviewScore    *********/
        public List<Doctor> SearchSimilar(Doctor doctor, Dictionary<string, List<Doctor>> dictionary) {
            string Specialty = doctor.Specialty;
            List<Doctor> spe_list = dictionary[Specialty];
            spe_list.Sort((x1, x2) => (Math.Abs(x1.Zipcode - doctor.Zipcode)/10 + x1.ReviewScore).CompareTo(Math.Abs(x2.Zipcode - doctor.Zipcode)/10 + x2.ReviewScore));
            return spe_list;
        }
    }
    #endregion

    #region Main Function
    class Program {
        static void Main(string[] args) {
            Doctor doctor = new Doctor();
            List<Doctor> lst = doctor.Create();

            #region Load all data into Hash Table
            Dictionary<string, List<Doctor>> dictionary = new Dictionary<string, List<Doctor>>();
            foreach(Doctor dc in lst) {
                if (dictionary.ContainsKey(dc.Specialty)) {
                    dictionary[dc.Specialty].Add(dc);
                }
                else {
                    dictionary.Add(dc.Specialty, new List<Doctor>() { dc});
                }
            }
            #endregion
                        
            //Test cases
            Doctor test1 = new Doctor();
            test1.Name = "James";
            test1.Specialty = "Allergy";
            test1.Zipcode = 30000;
            test1.ReviewScore = 4.1;
            Doctor test2 = new Doctor();
            test2.Name = "Madu";
            test2.Specialty = "Nutritionist";
            test2.Zipcode = 60000;
            Doctor test3 = new Doctor();
            test3.Name = "Bruce";
            test3.Specialty = "Emergency";
            test3.Zipcode = 81234;

            ///<summary>
            /// Here i created 3 test doctors, and with the SearchSimilar function
            /// it will filter the doctors with the same specialty, 
            /// i will sort the doctors with a hashed value
            /// based on the zipcode and viewscore
            ///</summary>
            ///<improvement>
            /// The prioritized order i think could focus not only on the specialty, 
            /// if we have the detailed description about the sympton, we can specific what part
            /// of the patient is care about. 
            ///</improvement>
            Search dosearch = new Search();
            List<Doctor> similar1 = dosearch.SearchSimilar(test1, dictionary);
            List<Doctor> similar2 = dosearch.SearchSimilar(test2, dictionary);
            List<Doctor> similar3 = dosearch.SearchSimilar(test3, dictionary);

            Console.WriteLine("*********Similar doctors with {0}*********", test1.Name);
            foreach (Doctor ds in similar1) {
                Console.Write(ds.Name + "\t");
            }
            Console.WriteLine();
            Console.WriteLine("*********Similar doctors with {0}*********", test2.Name);
            foreach (Doctor ds in similar2) {
                Console.Write(ds.Name + "\t");
            }
            Console.WriteLine();
            Console.WriteLine("*********Similar doctors with {0}*********", test3.Name);
            foreach (Doctor ds in similar3) {
                Console.Write(ds.Name + "\t");
            }
            Console.WriteLine();
        }
    }
    #endregion
}
