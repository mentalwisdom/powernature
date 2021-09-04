using System;
using System.ComponentModel.DataAnnotations;

namespace  nature.Models
{
    public class Course {
        [Key]
        public int courseId           {get;set;} //pk label="no"
        public string courseName     {get;set;} //label="course"
        public int fee {get;set;} //label="fee"
		public int discount {get;set;} //label="discount"
		public DateTime createdDate   {get;set;}
				

    }//ef
}//en