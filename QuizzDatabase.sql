USE master;

-- Xóa cơ sở dữ liệu Quizz
ALTER DATABASE Quizz SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE Quizz;

-- Tạo cơ sở dữ liệu Quizz
CREATE DATABASE Quizz;

-- Sử dụng cơ sở dữ liệu Quizz
USE Quizz;

-- Tạo bảng Quyen
CREATE TABLE Quyen (
    iMaQuyen int PRIMARY KEY,
    sTenQuyen nvarchar(50)
);
CREATE TABLE Khoa (
    sMaKhoa nvarchar(50) PRIMARY KEY,
    sTenKhoa nvarchar(30)
);
CREATE TABLE MonHoc (
    sMaMonHoc nvarchar(50) PRIMARY KEY,
    sTenMonHoc nvarchar(100),
    sMaKhoa nvarchar(50),
    CONSTRAINT FK_MonHoc_Khoa FOREIGN KEY (sMaKhoa) REFERENCES Khoa (sMaKhoa)
);
ALTER TABLE MonHoc
ADD tGianThi time;

CREATE TABLE CauHoi (
    iMaCauHoi int PRIMARY KEY IDENTITY(1,1),
    sNoiDungCauHoi nvarchar(500),
    sCauTraLoi1 nvarchar(200),
    sCauTraLoi2 nvarchar(200),
    sCauTraLoi3 nvarchar(200),
    sCauTraLoi4 nvarchar(200),
    iDapAn int,
    sMaMonHoc nvarchar(50),
    CONSTRAINT FK_CauHoi_MonHoc FOREIGN KEY (sMaMonHoc) REFERENCES MonHoc (sMaMonHoc)
);
-- Tạo bảng Khoa

-- Tạo bảng TaiKhoan
CREATE TABLE TaiKhoan (
    sMssv nvarchar(50) PRIMARY KEY,
    sMatKhau nvarchar(50),
	sHoTen nvarchar(100),
    sMaKhoa nvarchar(50),
    dNgaySinh date,
    sGioiTinh nvarchar(10),
    iMaQuyen int,
	AvatarID image,
    CONSTRAINT FK_TaiKhoan_Quyen FOREIGN KEY (iMaQuyen) REFERENCES Quyen (iMaQuyen),
    CONSTRAINT FK_TaiKhoan_Khoa FOREIGN KEY (sMaKhoa) REFERENCES Khoa (sMaKhoa),
);
ALTER TABLE TaiKhoan
ALTER COLUMN sMatKhau nvarchar(100);

create table Diem (
sMaMonHoc nvarchar(50) ,
sMssv nvarchar(50) ,
dDiem decimal(10, 2),
primary key(sMaMonHoc,sMssv),
foreign key(sMaMonHoc) references MonHoc(sMaMonHoc),
foreign key(sMssv) references TaiKhoan(sMssv)
);
INSERT INTO Quyen (iMaQuyen, sTenQuyen) VALUES 
(0, N'Admin'),
(1, N'User');

-- Thêm thông tin khoa vào bảng Khoa
INSERT INTO Khoa (sMaKhoa, sTenKhoa) VALUES 
(N'CNTT', N'Công Nghệ Thông Tin'),
(N'TOAN', N'Toán'),
(N'GDQP', N'Giáo Dục Quốc Phòng'),
(N'VT', N'Vật Lý'),
(N'HOA', N'Hóa Học'),
(N'KT', N'Kỹ Thuật'),
(N'ANH', N'Ngoại Ngữ'),
(N'KTMT', N'Kỹ Thuật Mạng'),
(N'CNPM', N'Công Nghệ Phần Mềm'),
(N'TTTT', N'Thông Tin Truyền Thông');

-- Thêm thông tin tài khoản vào bảng TaiKhoan
INSERT INTO TaiKhoan (sMssv, sMatKhau, sHoTen, sMaKhoa, dNgaySinh, sGioiTinh, iMaQuyen) VALUES 
(N'Admin', N'Admin', N'Nguyễn Văn A', N'CNTT', '2000-01-01', N'Nam', 0),
(N'User', N'User', N'Trần Thị B', N'CNTT', '2001-02-02', N'Nữ', 1),
(N'3', N'User321', N'Nguyễn Văn C', N'TOAN', '2002-03-03', N'Nam', 1),
(N'4', N'User456', N'Lê Thị D', N'TOAN', '2003-04-04', N'Nữ', 1),
(N'5', N'User789', N'Phạm Văn E', N'CNTT', '2000-05-05', N'Nam', 1),
(N'6', N'User654', N'Trần Văn F', N'TOAN', '2001-06-06', N'Nữ', 1),
(N'7', N'User987', N'Nguyễn Thị G', N'CNTT', '2002-07-07', N'Nam', 1),
(N'8', N'User123', N'Lê Văn H', N'TOAN', '2003-08-08', N'Nữ', 1),
(N'9', N'User234', N'Nguyễn Văn I', N'CNTT', '2000-09-09', N'Nam', 1),
(N'10', N'User345', N'Trần Thị J', N'TOAN', '2001-10-10', N'Nữ', 1),
(N'11', N'User456', N'Nguyễn Văn K', N'CNTT', '2002-11-11', N'Nam', 1),
(N'12', N'User567', N'Lê Thị L', N'TOAN', '2003-12-12', N'Nữ', 1),
(N'13', N'User678', N'Nguyễn Văn M', N'CNTT', '2000-01-13', N'Nam', 1),
(N'14', N'User789', N'Trần Thị N', N'TOAN', '2001-02-14', N'Nữ', 1),
(N'15', N'User890', N'Nguyễn Văn O', N'CNTT', '2002-03-15', N'Nam', 1);

INSERT INTO MonHoc (sMaMonHoc, sTenMonHoc, sMaKhoa) VALUES 
(N'MH001', N'Lập trình C++', N'CNTT'),
(N'MH002', N'Phát triển web', N'CNTT'),
(N'MH003', N'Khoa học dữ liệu', N'CNTT'),
(N'MH004', N'An ninh mạng', N'CNTT'),
(N'MH005', N'Triển khai phần mềm', N'CNTT'),

-- Môn học cho Khoa Toán
(N'MH006', N'Giải tích', N'TOAN'),
(N'MH007', N'Đại số tuyến tính', N'TOAN'),
(N'MH008', N'Lý thuyết số', N'TOAN'),
(N'MH009', N'Tối ưu hóa', N'TOAN'),
(N'MH010', N'Số học', N'TOAN'),

-- Môn học cho Khoa Giáo Dục Quốc Phòng
(N'MH011', N'Giáo dục thể chất', N'GDQP'),
(N'MH012', N'Training quân sự', N'GDQP'),
(N'MH013', N'Chiến lược quân sự', N'GDQP'),
(N'MH014', N'Tri thức quân sự', N'GDQP'),
(N'MH015', N'Thể lực quân sự', N'GDQP'),

-- Môn học cho Khoa Vật Lý
(N'MH016', N'Vật lý hạt nhân', N'VT'),
(N'MH017', N'Cơ học', N'VT'),
(N'MH018', N'Điện từ học', N'VT'),
(N'MH019', N'Quang học', N'VT'),
(N'MH020', N'Nhiệt động học', N'VT'),

-- Môn học cho Khoa Hóa Học
(N'MH021', N'Hóa vô cơ', N'HOA'),
(N'MH022', N'Hóa hữu cơ', N'HOA'),
(N'MH023', N'Hóa phân tích', N'HOA'),
(N'MH024', N'Hóa lý thuyết', N'HOA'),
(N'MH025', N'Hóa sinh', N'HOA'),

-- Môn học cho Khoa Kỹ Thuật
(N'MH026', N'Cơ khí', N'KT'),
(N'MH027', N'Kỹ thuật điện', N'KT'),
(N'MH028', N'Kỹ thuật điện tử', N'KT'),
(N'MH029', N'Kỹ thuật cơ khí', N'KT'),
(N'MH030', N'Thiết kế máy móc', N'KT'),

-- Môn học cho Khoa Ngoại Ngữ
(N'MH031', N'Tiếng Anh giao tiếp', N'ANH'),
(N'MH032', N'Tiếng Anh thương mại', N'ANH'),
(N'MH033', N'Ngữ pháp tiếng Anh', N'ANH'),
(N'MH034', N'Thực hành ngôn ngữ', N'ANH'),
(N'MH035', N'Trình bày tiếng Anh', N'ANH'),

-- Môn học cho Khoa Kỹ Thuật Mạng
(N'MH036', N'Quản trị mạng', N'KTMT'),
(N'MH037', N'Bảo mật mạng', N'KTMT'),
(N'MH038', N'Hệ thống thông tin mạng', N'KTMT'),
(N'MH039', N'Thiết kế mạng', N'KTMT'),
(N'MH040', N'Bảo trì hệ thống mạng', N'KTMT'),

-- Môn học cho Khoa Công Nghệ Phần Mềm
(N'MH041', N'Quản lý dự án phần mềm', N'CNPM'),
(N'MH042', N'Lập trình phần mềm', N'CNPM'),
(N'MH043', N'Kiểm thử phần mềm', N'CNPM'),
(N'MH044', N'Triển khai phần mềm', N'CNPM'),
(N'MH045', N'Quản lý yêu cầu phần mềm', N'CNPM'),

-- Môn học cho Khoa Thông Tin Truyền Thông
(N'MH046', N'Truyền thông kỹ thuật số', N'TTTT'),
(N'MH047', N'Quản lý thông tin', N'TTTT'),
(N'MH048', N'Truyền thông mạng xã hội', N'TTTT'),
(N'MH049', N'Quảng cáo trực tuyến', N'TTTT'),
(N'MH050', N'Tiếp thị kỹ thuật số', N'TTTT');
INSERT INTO CauHoi (sNoiDungCauHoi, sCauTraLoi1, sCauTraLoi2, sCauTraLoi3, sCauTraLoi4, iDapAn, sMaMonHoc) VALUES 
(N'C++ hỗ trợ lập trình theo mô hình nào?', N'Lập trình thủ tục', N'Lập trình hướng đối tượng', N'Lập trình hàm', N'Tất cả các phương pháp trên', 4, N'MH001'),
(N'Biến trong C++ được sử dụng để làm gì?', N'Lưu trữ dữ liệu', N'Thực hiện phép toán', N'Kiểm tra điều kiện', N'Tất cả các phương pháp trên', 4, N'MH001'),
(N'Từ khóa nào được sử dụng để định nghĩa một lớp trong C++?', N'class', N'struct', N'object', N'type', 1, N'MH001'),
(N'Trong C++, phương thức nào được gọi tự động khi một đối tượng được tạo?', N'Destructor', N'Constructor', N'Function', N'Method', 2, N'MH001'),
(N'Phương thức nào trong C++ được sử dụng để hủy đối tượng?', N'Destructor', N'Constructor', N'Delete', N'Remove', 1, N'MH001'),
(N'C++ có tính kế thừa không?', N'Có', N'Không', N'Chỉ một phần', N'Không chắc chắn', 1, N'MH001'),
(N'Trong C++, toán tử nào được sử dụng để gọi một hàm?', N'&', N'->', N'.', N'()', 4, N'MH001'),
(N'Khi nào thì biến toàn cục được sử dụng?', N'Trong một hàm', N'Trong tất cả các hàm', N'Chỉ trong lớp', N'Không bao giờ', 2, N'MH001'),
(N'Để nhập dữ liệu từ bàn phím, hàm nào được sử dụng trong C++?', N'read()', N'input()', N'cin', N'get()', 3, N'MH001'),
(N'Trong C++, từ khóa nào được sử dụng để xử lý ngoại lệ?', N'catch', N'throw', N'try', N'Tất cả các phương pháp trên', 4, N'MH001'),
(N'Kết quả của phép toán 5/2 trong C++ là gì?', N'2.0', N'2', N'2.5', N'3', 2, N'MH001'),
(N'Trong C++, từ khóa nào được sử dụng để tạo một con trỏ?', N'pointer', N'ref', N'*', N'&', 3, N'MH001'),
(N'Tại sao cần sử dụng hàm trong C++?', N'Để tiết kiệm thời gian', N'Để tổ chức mã nguồn', N'Để tăng tính tái sử dụng', N'Tất cả các phương pháp trên', 4, N'MH001'),
(N'Biểu thức nào sau đây là đúng để so sánh hai giá trị?', N'==', N'=', N'===', N'!=', 1, N'MH001'),
(N'Trong C++, cấu trúc điều khiển nào được sử dụng để kiểm tra nhiều điều kiện?', N'if', N'switch', N'for', N'while', 2, N'MH001'),
(N'Từ khóa nào được sử dụng để khai báo một biến hằng trong C++?', N'const', N'static', N'final', N'immutable', 1, N'MH001'),
(N'Trong C++, cú pháp nào dùng để định nghĩa một mảng?', N'array[]', N'[]array', N'type[] name', N'name[]type', 3, N'MH001'),
(N'Tính chất nào sau đây không phải của lập trình hướng đối tượng?', N'Đóng gói', N'Kế thừa', N'Đa hình', N'Thủ tục', 4, N'MH001'),
(N'C++ có hỗ trợ lập trình hàm không?', N'Có', N'Không', N'Chỉ một phần', N'Không chắc chắn', 1, N'MH001');


INSERT INTO CauHoi (sNoiDungCauHoi, sCauTraLoi1, sCauTraLoi2, sCauTraLoi3, sCauTraLoi4, iDapAn, sMaMonHoc) VALUES 
-- Lập trình web (MH002)
(N'Ngôn ngữ nào được sử dụng để tạo cấu trúc cho trang web?', N'HTML', N'CSS', N'JavaScript', N'PHP', 1, N'MH002'),
(N'CSS là viết tắt của?', N'Computer Style Sheets', N'Cascading Style Sheets', N'Creative Style Sheets', N'Colorful Style Sheets', 2, N'MH002'),
(N'Đâu không phải là một framework JavaScript phổ biến?', N'React', N'Angular', N'Vue', N'Java', 4, N'MH002'),
(N'Phương thức HTTP nào được sử dụng để gửi dữ liệu đến server?', N'GET', N'POST', N'PUT', N'DELETE', 2, N'MH002'),
(N'Công nghệ nào được sử dụng để lưu trữ dữ liệu cục bộ trong trình duyệt?', N'Local Storage', N'Session Storage', N'Cookies', N'Tất cả đều đúng', 4, N'MH002');
INSERT INTO CauHoi (sNoiDungCauHoi, sCauTraLoi1, sCauTraLoi2, sCauTraLoi3, sCauTraLoi4, iDapAn, sMaMonHoc) VALUES 
-- Khoa học dữ liệu (MH003)
(N'Thuật toán nào không thuộc về học máy có giám sát?', N'Linear Regression', N'K-means', N'Decision Trees', N'Logistic Regression', 2, N'MH003'),
(N'Pandas trong Python được sử dụng chủ yếu để làm gì?', N'Xử lý dữ liệu', N'Vẽ đồ thị', N'Machine Learning', N'Web Scraping', 1, N'MH003'),
(N'Đâu là một thư viện visualization phổ biến trong Python?', N'NumPy', N'Scikit-learn', N'Matplotlib', N'TensorFlow', 3, N'MH003'),
(N'Trong Big Data, 3V đại diện cho gì?', N'Volume, Variety, Velocity', N'Visibility, Value, Virtue', N'Validity, Veracity, Volatility', N'Vector, Variance, Variable', 1, N'MH003'),
(N'Kỹ thuật nào được sử dụng để giảm số chiều của dữ liệu?', N'PCA', N'K-means', N'SVM', N'Random Forest', 1, N'MH003'),

-- An ninh mạng (MH004)
(N'Phương pháp tấn công nào dựa vào việc giả mạo địa chỉ email?', N'Phishing', N'DDoS', N'Man-in-the-middle', N'SQL Injection', 1, N'MH004'),
(N'Giao thức nào được sử dụng để bảo mật kết nối web?', N'HTTP', N'FTP', N'HTTPS', N'SMTP', 3, N'MH004'),
(N'Firewall hoạt động ở tầng nào của mô hình OSI?', N'Physical', N'Network', N'Transport', N'Application', 2, N'MH004'),
(N'Kỹ thuật nào được sử dụng để mã hóa dữ liệu?', N'Encryption', N'Decryption', N'Hashing', N'Salting', 1, N'MH004'),
(N'Đâu không phải là một loại malware?', N'Virus', N'Trojan', N'Worm', N'Firewall', 4, N'MH004'),

-- Triển khai phần mềm (MH005)
(N'Mô hình phát triển phần mềm nào tập trung vào việc lặp lại các chu kỳ ngắn?', N'Waterfall', N'Agile', N'V-model', N'Spiral', 2, N'MH005'),
(N'Công cụ nào được sử dụng để quản lý version code?', N'Git', N'Docker', N'Jenkins', N'Kubernetes', 1, N'MH005'),
(N'Continuous Integration (CI) là gì?', N'Tự động test code', N'Tự động deploy', N'Tự động merge code', N'Tất cả đều đúng', 4, N'MH005'),
(N'Phương pháp nào không phải là một phương pháp kiểm thử phần mềm?', N'Unit Testing', N'Integration Testing', N'Regression Testing', N'Waterfall Testing', 4, N'MH005'),
(N'Công cụ nào được sử dụng để tạo môi trường ảo cho ứng dụng?', N'VMware', N'Docker', N'VirtualBox', N'Tất cả đều đúng', 4, N'MH005');
-- Xóa bảng theo thứ tự phụ thuộc
DROP TABLE TaiKhoan;
DROP TABLE Khoa;
DROP TABLE Quyen;
DROP TABLE MonHoc;
DROP TABLE CauHoi;

select * from Diem;