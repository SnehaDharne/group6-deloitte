﻿namespace Deloitte_Project.Models
{
    public class Metadata
    {
        public int Id { get; set; }
        public string file_name { get; set; }
        public long file_size { get; set; }
        public string sub_directory { get; set; }
        public string created_on { get; set; }
        public string created_by { get; set; }
    }
}
