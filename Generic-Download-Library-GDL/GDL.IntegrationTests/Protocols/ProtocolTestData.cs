using System;
using System.Collections.Generic;
using System.Text;

namespace GDL.IntegrationTests.Protocols {
	public static class ProtocolTestData {
		public const string TxtTestFileUrlHttp = "https://der-magere-student.com/public/gdl/test_files/test.txt";
		public const string TxtTestFileUrlFtp = "ftp://der-magere-student.com/public/gdl/test_files/test.txt";
		public const string TxtTestFileUrlFtps = "ftps://marcellorenz.sport-in-teltow.de//public/gdl/test_files/test.txt";

		public const string TxtTestFileContent = "If this is read out, the provider works fine!";

		public const string BinTestFile100MBUrlHttp = "https://der-magere-student.com/public/gdl/test_files/100MB.bin";
		public const string BinTestFile100MBUrlFtp = "ftp://der-magere-student.com/public/gdl/test_files/100MB.bin";
		public const string BinTestFile100MBUrlFtps = "ftps://marcellorenz.sport-in-teltow.de//public/gdl/test_files/100MB.bin";
	}
}