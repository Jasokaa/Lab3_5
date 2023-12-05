using BLL;
using DAL;
namespace BLLTests
{
    [TestClass]
    public class EntityServiceTests
    {
        private EntityService<Student> entityService;

        [TestInitialize]
        public void TestInitialize()
        {
            entityService = new EntityService<Student>();
        }
        [TestMethod]
        public void AddEntity()
        {
            // Arrange
            List<Student> list = new List<Student>();
            Student input = new Student("Tom", "Rox", 4, "KB12312312", "03.03.2002");
            // Act
            List<Student> result = entityService.AddEntity(list, input);
            // Assert
            CollectionAssert.AreNotEqual(list, null);
        }
        [TestMethod]
        public void RemoveEntityByIndex_DoesNotContain()
        {
            // Arrange
            Student s = new Student("Tom", "Rox", 4, "KB12312312", "03.03.2002");
            List<Student> list = new List<Student> { s };
            int indexToRemove = 0;
            // Act
            List<Student>? result = entityService.RemoveEntityByIndex(list, indexToRemove);
            // Assert
            CollectionAssert.DoesNotContain(result, s);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RemoveEntityByIndex_IndexException()
        {
            // Arrange
            Student s = new Student("Tom", "Rox", 4, "KB12312312", "03.03.2002");
            List<Student> list = new List<Student> { s };
            int invalidIndex = 5; // Invalid index
            // Act & Assert
            entityService.RemoveEntityByIndex(list, invalidIndex);
        }
        [TestMethod]
        public void InputName()
        {
            // Arrange
            // Act& Assert
            Assert.IsTrue(entityService.InputName("John"));
            Assert.IsFalse(entityService.InputName("john"));
        }
        [TestMethod]
        public void InputCourse()
        {
            // Arrange
            // Act & Assert
            Assert.IsTrue(entityService.InputCourse("2"));
            Assert.IsFalse(entityService.InputCourse("8"));
        }
        [TestMethod]
        public void InputStudentCard()
        {
            // Arrange
            // Act & Assert
            Assert.IsTrue(entityService.InputStudentCard("KB12345678"));
            Assert.IsFalse(entityService.InputStudentCard("FG12345678"));
        }
        [TestMethod]
        public void InputBirthDate()
        {
            // Arrange
            // Act & Assert
            Assert.IsTrue(entityService.InputBirthDate("01.02.2000"));
            Assert.IsFalse(entityService.InputBirthDate("01.12.20000"));
            Assert.IsFalse(entityService.InputBirthDate("30.02.2000"));
            Assert.IsFalse(entityService.InputBirthDate("3d.01.2000"));
        }
        [TestMethod]
        public void SearchStudent_DoesNotContain()
        {
            // Arrange
            Student s1 = new Student("Tom", "Rox", 4, "KB12312312", "03.03.2002");
            Student s2 = new Student("Kate", "Lu", 4, "KB09809809", "30.05.2001");
            Student s3 = new Student("Bob", "Wein", 4, "KB89898989", "20.11.2003");
            List<Entity> studentList = new List<Entity>
            {
                s1, s2, s3
            };
            // Act
            List<Student> result = entityService.SearchStudent(studentList);
            // Assert
            CollectionAssert.DoesNotContain(result, s3);
        }
        [TestMethod]
        public void SearchStudent_Count()
        {
            // Arrange
            Student s1 = new Student("Tom", "Rox", 4, "KB12312312", "03.03.2002");
            Student s2 = new Student("Kate", "Lu", 3, "KB09809809", "30.05.2001");
            Student s3 = new Student("Bob", "Wein", 4, "KB89898989", "20.11.2003");
            List<Entity> studentList = new List<Entity>
            {
                s1, s2, s3
            };
            // Act
            List<Student> result = entityService.SearchStudent(studentList);
            // Assert
            Assert.AreEqual(1, result.Count);
        }
        [TestMethod]
        public void SearchSpring_Valid()
        {
            // Arrange
            Student s1 = new Student("Tom", "Rox", 4, "KB12312312", "03.03.2002");
            // Act
            bool result = entityService.SearchSpring(s1);
            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void SearchSpring_Invalid()
        {
            // Arrange
            Student s3 = new Student("Bob", "Wein", 4, "KB89898989", "20.11.2003");
            // Act
            bool result = entityService.SearchSpring(s3);
            // Assert
            Assert.IsFalse(result);
        }
    }
    [TestClass]
    public class DBServiceTests
    {
        DBService<Entity> dbService;

        [TestInitialize]
        public void TestInitialize()
        {
            dbService = new DBService<Entity>();
        }
        [TestMethod]
        public void WriteDB()
        {
            // Arrange
            
            Student s1 = new Student("Tom", "Rox", 4, "KB12312312", "03.03.2002");
            Student s2 = new Student("Kate", "Lu", 3, "KB09809809", "30.05.2001");
            Student s3 = new Student("Bob", "Wein", 4, "KB89898989", "20.11.2003");
            List<Entity>? list = new List<Entity>
            {
                s1, s2, s3
            };
            // Act
            dbService.DeleteAllFromFile();
            dbService.WriteDB(list);
            // Assert
            List<Entity>? readData = new List<Entity>();
            readData = dbService.ReadDB();
            Assert.AreEqual(list.Count, readData.Count);
            CollectionAssert.AreEqual(list, readData);
        }
        [TestMethod]
        public void WriteDB_NullList()
        {
            // Arrange
            // Act
            dbService.DeleteAllFromFile();
            dbService.WriteDB(null);
            // Assert
            List<Entity> readData = dbService.ReadDB();
            Assert.IsNull(readData);
        }
        [TestMethod]
        public void WriteDB_EmptyList()
        {
            // Arrange
            List<Entity> emptyList = new List<Entity>();
            // Act
            dbService.DeleteAllFromFile();
            dbService.WriteDB(emptyList);
            // Assert
            List<Entity> readData = dbService.ReadDB();
            Assert.AreEqual(0, readData?.Count ?? 0);
        }
        
        [TestMethod]
        public void ReadDB_EmptyFile()
        {
            // Arrange
            // Act
            dbService.DeleteAllFromFile();
            // Assert
            List<Entity>? actualData = dbService.ReadDB();
            Assert.IsNull(actualData, "Reading from an empty file should return null.");
        }
        [TestMethod]
        public void DeleteAllFromFile()
        {
            // Arrange
            List<Entity> initialData = new List<Entity>
            {
                new Student("John", "Doe", 1, "KB12345678", "01.01.2000"),
                new Student("Alice", "Smith", 2, "KB87654321", "05.05.1999")
            };
            // Act
            dbService.WriteDB(initialData);
            dbService.DeleteAllFromFile();
            // Assert
            List<Entity>? readData = dbService.ReadDB();
            Assert.IsNull(readData);
        }
    }
    [TestClass]
    public class CreateServiceTests
    {
        CreateService createService;
        DBService<Entity> dbService;
        [TestInitialize]
        public void TestInitialize()
        {
            createService = new CreateService();
            dbService = new DBService<Entity>();
        }
        [TestMethod]
    public void CreateStudent()
    {
        // Arrange
        // Act
        createService.createStudent("Tom", "Rox", 4, "KB12312312", "03.03.2002");
        // Assert
        List<Entity>? readData = dbService.ReadDB();
        Assert.IsNotNull(readData);
        Student? createdStudent = readData.Find(s => s is Student) as Student;
        Assert.IsNotNull(createdStudent);
        Assert.AreEqual("Tom", createdStudent.FirstName);
        Assert.AreEqual("Rox", createdStudent.LastName);
        Assert.AreEqual(4, createdStudent.Course);
        Assert.AreEqual("KB12312312", createdStudent.StudentCard);
        Assert.AreEqual("03.03.2002", createdStudent.BirthDate);
    }
    [TestMethod]
    public void CreateBaker()
    {
        // Arrange
        // Act
        createService.createBaker("John", "Doe");
        // Assert
        List<Entity>? readData = dbService.ReadDB();
        Assert.IsNotNull(readData);
        Baker? createdBaker = readData.Find(b => b is Baker) as Baker;
        Assert.IsNotNull(createdBaker);
        Assert.AreEqual("John", createdBaker.FirstName);
        Assert.AreEqual("Doe", createdBaker.LastName);
    }

    [TestMethod]
    public void CreateEntrepreneur()
    {
        // Arrange
        // Act
        createService.createEntrepreneur("Alice", "Smith");
        // Assert
        List<Entity>? readData = dbService.ReadDB();
        Assert.IsNotNull(readData);
        Entrepreneur? createdEntrepreneur = readData.Find(e => e is Entrepreneur) as Entrepreneur;
        Assert.IsNotNull(createdEntrepreneur);
        Assert.AreEqual("Alice", createdEntrepreneur.FirstName);
        Assert.AreEqual("Smith", createdEntrepreneur.LastName);
    }
    }
}