//using System;
//using System.Linq;
//using System.Text;
//using SharpFuzz;                // <- for AFL
//// or: using SharpFuzz.LibFuzzer;   // <- for libFuzzer

//public static class LoginFuzz
//{
//    [Fuzz]
//    public static void FuzzLogin(byte[] data)
//    {
//        var payload = Encoding.UTF8.GetString(data);

//        var dto = new Erp_V1.BLL.EmployeeBLL().Select();
//        var _ = dto.Employees.Any(x =>
//            x.UserNo.ToString() == payload &&
//            x.Password == payload
//        );
//    }
//}
