namespace HttpClient_API_TestFramework.Model
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool isActive { get; set; }
        public bool stEquals(Student st)
        {
            return this.StudentId == st.StudentId &&
                    this.Email == st.Email &&
                    this.FirstName == st.FirstName &&
                    this.LastName == st.LastName &&
                    this.Phone == st.Phone &&
                    this.isActive == st.isActive;
        }
    }
}
