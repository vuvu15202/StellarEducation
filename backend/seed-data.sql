USE [DonationWebApp_v4]

delete from [StudentFee]
delete from [CourseEnroll]
delete from [UserRole]
delete from [Lesson]
delete from [Course]
delete from [Category]
delete from [Review]
delete from [Role]
delete from [User]
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [Name], [Image]) VALUES (1, N'Làm quen với Cơ Sở Dữ Liệu', N'')
INSERT [dbo].[Category] ([CategoryID], [Name], [Image]) VALUES (2, N'Lập trình game với Unity', N'')
INSERT [dbo].[Category] ([CategoryID], [Name], [Image]) VALUES (3, N'Lộ trình BackEnd', N'')
INSERT [dbo].[Category] ([CategoryID], [Name], [Image]) VALUES (4, N'Lộ trình FrontEnd', N'')
INSERT [dbo].[Category] ([CategoryID], [Name], [Image]) VALUES (5, N'FullStack Development', N'')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (2, 1, N'SQLServer Cơ Bản', N'/images/courses/SQLServerCoBan.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 0, 0)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (3, 1, N'MySQL Cơ Bản', N'/images/courses/MySQCoBan.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 0, 0)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (4, 1, N'SQLServer Nâng Cao', N'/images/courses/MySQNangCao.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 299000)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (5, 1, N'MySQL Nâng Cao', N'/images/courses/MySQNangCao.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 299000)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (6, 2, N'Unity 2D Nâng Cao', N'/images/courses/Unity2DNangCao.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 299000)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (7, 2, N'Unity 3D Nâng Cao', N'/images/courses/Unity3DNangCao.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 299000)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (8, 2, N'Unity 3D Cơ bản', N'/images/courses/Unity3DCoBan.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 0)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (9, 2, N'Unity 2D Cơ bản', N'/images/courses/Unity2DCoBan.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 0)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (10, 3, N'Lập Trình Java Cơ Bản', N'/images/courses/LapTrinhJavaCoBan.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 0)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (11, 3, N'Lập Trình Java với SpringBoot', N'/images/courses/LapTrinhJavaVoiSpringBoot.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 399000)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (12, 3, N'Lập Trình C# cơ bản', N'/images/courses/LapTrinhCSharpCoBan.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 0)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (13, 3, N'Lập Trình Windows Form với .NET CORE', N'/images/courses/LapTrinhWindowsFormvoidotnetcore.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 399000)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (14, 3, N'Lập Trình WPF với .NET CORE', N'/images/courses/LapTrinhWPFvoidotnetcore.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 399000)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price]) VALUES (15, 3, N'Lập Trình RestFul API với ASP.NET CORE', N'/images/courses/LapTrinhRestfulAPIvoiAspdotnetcore.png', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', 1, 399000)
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
SET IDENTITY_INSERT [dbo].[FundraisingProject] ON 

INSERT [dbo].[FundraisingProject] ([ProjectId], [Type], [Title], [Story], [Image], [TargetAmount], [StartDate], [EndDate], [Discontinued]) VALUES (3, 1, N'Chung tay gây quỹ trồng 1.000 cây bần chua và phát động trồng cây bảo vệ môi trường tại rừng ngập mặn tại Cù Lao Dung, Sóc Trăng (Đợt 2)', N'<h2 class="py-5 text-xl font-semibold">Câu chuyện</h2>
<article class="soju__prose small">
    <p style="font-size:17px">
        Đồng bằng Sông Cửu Long là mảnh đất nằm ở cuối dòng chảy của sông Mekong trước khi đổ vào Biển Đông. Đây là một vùng đất thấp và bằng phẳng, mà những biến đổi khí hậu đã gây ra những ảnh hưởng nặng nề. Theo Ngân hàng Thế giới, Việt Nam nằm trong nhóm 5 quốc gia chịu ảnh hưởng nặng nề do biến đổi khí hậu. Những nguyên nhân như triều cường, sạt lở bờ biển, và bờ sông đã gây thiệt hại đáng kể cho rừng ngập mặn ven biển, và điều này đang ảnh hưởng đến kế hoạch phát triển kinh tế - xã hội của các địa phương, trong đó có tỉnh Sóc Trăng.
    </p>

    <p style="font-size:17px">
        <img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240318152309-638463721896812649.jpg" style="width: 100%;">
    </p>

    <p style="font-size:17px">
        <img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240318152324-638463722046373682.jpg" style="width: 100%;">
    </p>

    <p style="text-align: center;">
        <em>Tình hình thiên tai sạt lở bờ sông, bờ kênh gây thiệt hại nghiêm trọng cho người dân</em>
    </p>

    <p style="font-size:17px">
        Cù Lao Dung là huyện cực Đông của tỉnh Sóc Trăng, nằm cuối nguồn dòng sông Hậu thơ mộng, với hai cửa Định An và Trần Đề đổ ra biển Đông. Những năm gần đây, tình hình thiên tai sạt lở bờ sông, bờ kênh gây thiệt hại đường giao thông nông thôn, nhà cửa, cơ sở hạ tầng công cộng… trên địa bàn huyện xảy ra ngày càng nhiều. Cao điểm nhất là vào tháng mưa, lũ, triều cường. Thống kê của Chi cục Thủy lợi tỉnh Sóc Trăng cho thấy từ năm 2019, riêng bờ sông Hậu trung bình mỗi năm sạt lở chiều dài khoảng 500 - 1.000m. Tính từ đầu năm 2022 đến nay, sạt lở bờ sông Hậu tiếp tục diễn biến phức tạp, qua khảo sát đã có khoảng 30 điểm sạt lở nguy hiểm, với tổng chiều dài trên 1.500m thuộc địa bàn xã Đại Ân 1 và An Thạnh Đông, làm vỡ bờ bao nuôi tôm của dân phía ngoài đê và lấn sâu vào sạt lở hết chân và mái để bao Tả, Hữu Cù Lao Dung.
    </p>

    <p style="font-size:17px">
        <img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240318152352-638463722329142269.jpg" style="width: 100%;">
    </p>

    <p style="font-size:17px">
        <img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240318152409-638463722497897512.jpg" style="width: 100%;">
    </p>

    <p style="text-align: center;">
        <em>Mỗi hecta rừng ngập mặn chứa lượng carbon cao gấp 4 lần so với các khu rừng mưa nhiệt đới khác</em>
    </p>

    <p style="font-size:17px">
        Nhận thức về thực trạng của khu vực Cù Lao Dung và tầm quan trọng của việc trồng rừng, MSD United Way Vietnam thực hiện dự án "Đại sứ Môi trường - Trồng 1 ha cây bần chua tại khu vực Cù Lao Dung, tỉnh Sóc Trăng". Rừng ngập mặn đã được Nhà nước quan tâm đặc biệt từ nhiều năm trước. Theo một nghiên cứu, mỗi hecta rừng ngập mặn chứa lượng carbon cao gấp 4 lần so với các khu rừng mưa nhiệt đới khác. Sau khi tiến hành nghiên cứu và đánh giá hoạt động trồng rừng tại tỉnh Sóc Trăng, chúng tôi quyết định thực hiện dự án tại Huyện Cù Lao Dung với những hoạt động cụ thể:
    </p>

    <ul>
        <li aria-level="1" style="font-size: 14px">Trồng 1 ha cây bần tại khu vực Cù Lao Dung (khoảng 2.500 cây).</li>
        <li aria-level="1" style="font-size: 14px">Báo cáo tác động hàng năm (trong vòng 2 năm).</li>
        <li aria-level="1" style="font-size: 14px">Trồng lại 20% nếu cây chết.</li>
        <li aria-level="1" style="font-size: 14px">Phát động truyền thông bảo vệ môi trường nhân Ngày Trái Đất 22.4.</li>
    </ul>

    <p style="font-size:17px">
        Dự án "Đại sứ Môi trường - Trồng 1 ha cây bần chua tại khu vực Cù Lao Dung, tỉnh Sóc Trăng" sẽ hỗ trợ người dân địa phương rất nhiều trong việc cản gió, tránh bão và tránh sạt lở đất. Quan trọng hơn, dự án góp phần hưởng ứng chiến lược quốc gia về phòng, chống thiên tai quốc gia, hưởng ứng Ngày Trái Đất 22.4.
    </p>

    <p>
        <img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240318152452-638463722923072183.jpg" style="width: 100%;">
    </p>

    <p style="text-align: center;">
        <em>Chung tay gây quỹ hoàn thành mục tiêu trồng 2.500 cây bần chua của dự án cải tạo rừng tại Cù Lao Dung</em>
    </p>

    <p style="font-size:17px">
        Vào tháng 11/2023 chúng tôi đã kêu gọi thành công đợt 1 với 1.500 cây bần chua và để dự án được tiếp tục tiến hành, Siêu ứng dụng MoMo kết hợp cùng MSD United Way Vietnam kêu gọi thêm sự hỗ trợ từ cộng đồng để trồng thêm 1.000 cây để đạt mục tiêu của dự án là trồng được 2.500 cây, tương đương với 1 hecta rừng ngập mặn. Chúng tôi mong muốn các nhà hảo tâm, các mạnh thường quân cùng chung tay để đạt mục tiêu gây quỹ 200.000.000 đồng. Số tiền này sẽ được sử dụng để mua cây giống, vận chuyển cây, chi trả nhân công, chi phí hành chính, theo dõi và quản lý dự án, tổ chức phát động và truyền thông trồng rừng, hỗ trợ chi phí truyền thông, gây quỹ và tạo nhận thức… Chúng tôi tin rằng dự án này sẽ thành công và hiệu quả hơn nếu có sự đóng góp chung từ cộng đồng. Hãy cùng chung tay bảo vệ Sóc Trăng và tạo một tương lai bền vững hơn cho mọi người!
    </p>

    <p style="font-size:17px">
        <u><strong>Về MSD United Way Vietnam:</strong></u><br>
        MSD United Way Vietnam là tổ chức phi lợi nhuận Việt Nam và là thành viên của United Way Worldwide - mạng lưới phi lợi nhuận toàn cầu hoạt động hơn&nbsp;135 năm tại hơn 40 quốc gia và vùng lãnh thổ.&nbsp;MSD United Way Việt Nam hoạt động với mục tiêu nâng cao chất lượng giáo dục, cải thiện thu nhập và đảm bảo cuộc sống khỏe mạnh cho nhóm người yếu thế&nbsp;trong xã hội như trẻ em, thanh niên, phụ nữ, người dân tộc thiểu số, người khuyết tật,… bằng cách huy động sự quan tâm và gắn kết sức mạnh của cộng đồng.
    </p>
</article>', N'https://homepage.momocdn.net/blogscontents/momo-upload-api-240318152215-638463721352578327.jpg', 2000000, CAST(N'2024-03-13T00:00:00.000' AS DateTime), CAST(N'2024-06-13T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[FundraisingProject] ([ProjectId], [Type], [Title], [Story], [Image], [TargetAmount], [StartDate], [EndDate], [Discontinued]) VALUES (4, 1, N'Gây quỹ xây dựng 7 thư viện dành tặng cho 3.150 trẻ em vùng khó khăn thuộc tỉnh Đắk Lắk, Gia Lai và Huyện Củ Chi', N'<h2 class="py-5 text-xl font-semibold">Câu chuyện</h2>
<article class="soju__prose small"><p>
	<em>“Mai này lớn lên con muốn trở thành thầy giáo” hay “Con muốn trở thành người có ích cho xã hội"</em> là hai trong rất nhiều ước mơ của những em nhỏ tại các khu vực khó khăn và còn nhiều thiếu thốn về cơ sở hạ tầng phục vụ giáo dục. Học để thành nhân, học để thành tài, học để chạm đến những ước mơ, góp phần xây dựng xã hội giàu mạnh. Nhưng không phải ai cũng dễ dàng chạm được đến cánh cửa tri thức, đặc biệt là các em học sinh vùng sâu vùng xa, vùng nông thôn.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240314133209-638460199290088691.jpg" style="width: 100%;">
</p>

<p style="text-align: center;">
	<em>Những ước mơ bị gác lại bởi ở địa bàn vùng sâu các em ít được tiếp cận với sách - nguồn tri thức vô tận</em>
</p>

<p>
	<strong>Học sinh ở địa bàn vùng sâu, vùng xa của tỉnh Đắk Lắk và Gia Lai gặp nhiều khó khăn&nbsp;&nbsp;</strong>
</p>

<p>
	Theo Sở GDĐT tỉnh Đắk Lắk, năm học 2022 - 2023, địa phương có hơn 484.000 học sinh các cấp, trong đó gần 35% là người đồng bào dân tộc thiểu số. Ở những vùng còn khó khăn, người dân nhận thức vẫn hạn chế, phụ huynh rất ít quan tâm đến việc học hành của con em nên chất lượng học tập còn rất thấp. Thậm chí có người còn không cho con em đến tuổi đi học đến trường hoặc để các cháu bỏ học giữa chừng, ở nhà lập gia đình hoặc làm nương rẫy.&nbsp;
</p>

<p>
	Ở vùng sâu vùng xa của Gia Lai và Đắk Lắk, học sinh đang đối mặt với những khó khăn do thiếu hụt cơ sở vật chất, thư viện và tài liệu. Điều này ảnh hưởng đáng kể đến quyền tiếp cận tri thức và sự phát triển học tập của các em. Thiếu hụt thư viện và tài liệu là một vấn đề đáng lo ngại vì nó hạn chế khả năng tiếp thu và nghiên cứu của học sinh, khiến cho việc mở rộng kiến thức và phát triển cá nhân trở nên khó khăn hơn.
</p>

<p>
	<strong>Củ Chi – huyện ngoại thành của T.p Hồ Chí Minh vẫn còn nhiều khó khăn chồng chất khó khăn</strong>
</p>

<p>
	Nơi được xem là “vùng trắng” cách mạng, Củ Chi có số hộ gia đình nghèo và cận nghèo ở các vùng nông thôn khá cao. Không chỉ các hộ dân mà các em học sinh ở đây cũng gặp không ít khó khăn trong việc đến trường. Ở đây học sinh đang đối mặt với nhiều khó khăn do thiếu hụt cơ sở vật chất, thư viện và tài liệu tham khảo. Với số lượng sách và tài liệu hạn chế, học sinh không có cơ hội tiếp cận đủ nguồn thông tin phong phú và đa dạng. Điều này ảnh hưởng tiêu cực đến khả năng nghiên cứu và tìm hiểu và sự phát triển toàn diện của các em. Tại các cơ sở giáo dục: khó khăn về cơ sở vật chất, tiền lương eo hẹp...khiến giáo viên ngoại thành bỏ trường mà đi. Mặt khác, ở vùng sâu vùng xa, khi phụ huynh và học sinh chưa ý thức hết vai trò của việc học vì nhiều lý do, sự lệch pha về chuyên môn của giáo viên càng hạn chế chất lượng giáo dục ở những khu vực này.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240314133301-638460199819143318.jpg" style="width: 100%;">
</p>

<p style="text-align: center;">
	<em>Mỗi thư viện được xây sẽ góp phần thắp sáng thêm những ước mơ cho các em học sinh&nbsp;</em>
</p>

<p>
	Khác biệt hoàn toàn với những ngôi trường khang trang, xây dựng kiên cố, cơ sở vật chất đầy đủ: có thư viện, phòng y tế, sân chơi, được trang bị những thiết bị dạy học hiện đại ở thành phố; những lớp học ở vùng sâu, vùng xa thiếu thốn, khó khăn trăm bề. Điều này ảnh hưởng rất lớn đến chất lượng giáo dục mà các em xứng đáng nhận được.&nbsp;
</p>

<p>
	<strong>Chung tay xây dựng 7 thư viện cho các em học sinh nghèo các tỉnh Đắk Lắk, Gia Lai và huyện Củ Chi</strong>
</p>

<p>
	Hiểu được sự khó khăn của các em học sinh nơi đây, Siêu ứng dụng MoMo kết hợp cùng dự án Thư Viện Ước Mơ mong muốn kêu gọi cộng đồng các nhà hảo tâm, các mạnh thường quân cùng chung tay quyên góp số tiền là 150.000.000 đồng. Số tiền này sẽ được sử dụng để xây dựng 7 thư viện ước mơ cho các em học sinh tiểu học thuộc vùng sâu vùng xa của tỉnh Đắk Lắk, Gia Lai và huyện Củ Chi. Chúng tôi sẽ mang đến những trang sách chứa đầy ước mơ đến với các em để hy vọng rằng các em trò nhỏ cứ tiếp tục lớn hơn và hạnh phúc với nó.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240314133349-638460200294491800.jpg" style="width: 100%;">
</p>

<p style="text-align: center;">
	<em>Gây quỹ mang đến thư viện mới, cuốn sách mới và không gian đọc mới cho các em học sinh ở những vùng còn thiếu thốn cơ sở vật chất</em>
</p>

<p>
	Tổng kinh phí dự kiến của dự án là 750 triệu đồng, vì vậy chúng tôi còn có 600 triệu đồng được các nhà tài trợ quy đổi từ chiến dịch quyên góp Heo Vàng.&nbsp;
</p>

<p>
	Thư Viện Ước Mơ tin rằng, có thêm thư viện mới, cuốn sách mới và không gian đọc mới sẽ giúp các em dân tộc thiểu số và các em ở vùng sâu vùng xa, vùng còn khó khăn có cơ hội được tiếp cận với sách - nguồn sáng tạo vô tận. Vì vậy dự án Thư Viện Ước Mơ rất cần sự chung tay của cộng động để cùng xây dựng ước mơ mang tri thức đến với thế hệ Búp Măng Việt Nam. Để tiếp nối 10 năm, 20 năm nữa, thế hệ trẻ Việt Nam sẽ toàn những công dân sáng tạo toàn cầu với những phẩm chất ưu tú.<br>
	<br>
	<u><strong>Thư Viện Ước Mơ - Library of Dreams:</strong></u><br>
	Là một dự án THƯ VIỆN SÁNG TẠO dành cho trẻ em Việt Nam ở những vùng còn khó tiếp cận với sách và các hoạt động văn hóa, nghệ thuật, sáng tạo. Dự án do bà Nguyễn Phi Vân - Chủ tịch Hiệp Hội đầu tư thiên thần Đông Nam Á&nbsp; sáng lập năm 2014. Đến năm 2017, dự án được nâng cấp thành doanh nghiệp xã hội Thư viện Ước mơ. Thư viện sáng tạo là nơi nuôi dưỡng trí tưởng tượng, giúp các em tiếp cận với các nguồn tri thức khắp nơi trên thế giới và gieo mầm ước mơ cho các em trở thành những công dân toàn cầu thế kỷ 21.&nbsp;&nbsp;
</p>

<p>
	Tính đến hết tháng 12 năm 2023, dự án đã xây dựng được 237 thư viện ước mơ, tại hơn 33&nbsp; tỉnh thành khắp Việt Nam: Bến Tre, Bình Phước, Đăk Lăk, Đăk Nông, Đồng Nai, Hà Giang, Hòa Bình, xã đảo Cần Giờ- HCM, Kon Tum,&nbsp; Lâm Đồng, Quảng Nam, Quảng Ngãi, Sơn La, Tây Ninh, Thanh Hóa, Tuyên Quang, Vĩnh Long, Bà Rịa - Vũng Tàu, Yên Bái, Bắc Giang, Bắc Ninh, Bình Phước, Tiền Giang, Đồng Tháp, Phú Thọ, Vĩnh Phúc… với số học sinh tiếp cận được sách &amp; các hoạt động nghệ thuật: hơn 133.200 học sinh&nbsp;
</p>

<p>
	Tâm nguyện dự là mong ước xây dựng 1.000 Thư Viện tác động 1.000.000 học sinh Việt Nam vào 2030.
</p>
</article>', N'https://homepage.momocdn.net/blogscontents/momo-upload-api-240314133121-638460198819517920.jpg', 30000000, CAST(N'2024-03-14T00:00:00.000' AS DateTime), CAST(N'2024-06-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[FundraisingProject] ([ProjectId], [Type], [Title], [Story], [Image], [TargetAmount], [StartDate], [EndDate], [Discontinued]) VALUES (5, 2, N'Gây quỹ trao học bổng cho 10 em học sinh có hoàn cảnh khó khăn và trang thiết bị cho trường Tiểu học Trung Lương, Hà Nam', N'<h2 class="py-5 text-xl font-semibold">Câu chuyện</h2>
	<article class="soju__prose small"><p>
	Trường Tiểu học Trung Lương là một ngôi trường nhỏ tại thôn Đồng Quan, xã Trung Lương, huyện Bình Lục, tỉnh Hà Nam. Hiện trường có tổng cộng là 16 lớp với 643 em học sinh đang theo học tại đây. Cơ sở vật chất của trường như phòng học, bàn ghế về cơ bản đã đáp ứng được nhu cầu giáo dục. Tuy nhiên, với xu hướng đổi mới phương pháp giáo dục, áp dụng các bài giảng điện tử vào học tập, nhà trường vẫn chưa có đủ máy tính và tivi để đáp ứng nhu cầu này.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240312140621-638458491813388367.jpg" style="width: 100%;">
</p>

<p style="text-align: center;">
	<em>Trường Tiểu học xã Trung Lương là nơi theo học của các em học sinh tại thôn Đồng Quan, xã Trung Lương, huyện Bình Lục, tỉnh Hà Nam&nbsp;</em>
</p>

<p>
	Bên cạnh đó, thu nhập của người dân vùng chiêm trũng tại thôn Đồng Quan thuộc xã Trung Lương, huyện Bình Lục, tỉnh Hà Nam chủ yếu dựa vào sản xuất nông nghiệp. Vốn là vùng rốn nước hạ lưu châu thổ sông Hồng, khó canh tác, vì thế dù có nhiều nỗ lực trong quá trình cải thiện kinh tế gia đình nhưng nhiều hộ dân vẫn chưa thể vươn lên thoát nghèo.&nbsp;
</p>

<p>
	Trong trường vẫn còn rất nhiều em học sinh có hoàn cảnh đặc biệt khó khăn như mồ côi cha hoặc mẹ; cha hoặc mẹ đang mắc những bệnh hiểm nghèo. Gia đình các em được chứng nhận là hộ nghèo của xã, cần phải có sự quan tâm hỗ trợ đặc biệt từ nhà trường để các em có thể đảm bảo tiếp tục việc học.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240312140649-638458492099909760.jpg" style="width: 100%;">
</p>

<p style="text-align: center;">
	<em>Trong trường vẫn còn rất nhiều em học sinh có hoàn cảnh đặc biệt khó khăn</em>
</p>

<p>
	Ghé thăm trường Tiểu học Trung Lương vào những ngày đầu năm, chẳng khó để bắt gặp hình ảnh những đứa trẻ nô đùa hạnh phúc trong giờ ra chơi hay âm thanh ê a đọc bài vang vọng trong những lớp học. Thế nhưng chẳng biết những đứa trẻ vô tư ấy có nhận biết được hoàn cảnh còn nhiều bấp bênh của gia đình mình hay chăng, chỉ biết các em vẫn luôn kiên trì đến trường đến lớp, vẫn chăm chỉ học hành và luôn nghe lời thầy cô giáo.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240312140716-638458492369896480.jpg" style="width: 100%;">
</p>

<p style="text-align: center;">
	<em>Chung tay gây quỹ trao học bổng cho các em học sinh nghèo vượt khó để cổ vũ tinh thần học tập</em>
</p>

<p>
	Đồng cảm trước những khó khăn vất vả của bà con và các em trò nhỏ tại thôn Đồng Quan, xã Trung Lương, huyện Bình Lục, tỉnh Hà Nam Siêu ứng dụng MoMo kết hợp cùng VIGEF kêu gọi cộng đồng các nhà hảo tâm, các mạnh thường quân trên khắp cả nước cùng chung tay gây quỹ với số tiền là 100.000.000 đồng. Số tiền sẽ được sử dụng để trao 10 suất học bổng cho 10 em học sinh có hoàn cảnh đặc biệt khó khăn, trao tặng ít nhất 07 chiếc Tivi 50 inch cho 07 phòng học để phục vụ cho việc dạy và học của thầy cô và các em học sinh. Ngoài ra còn sử dụng vào chi phí vận chuyển, lắp đặt tivi cùng các chi phí khác như chi phí truyền thông, gây quỹ, theo dõi và quản lý dự án...&nbsp;
</p>

<p>
	Chúng tôi tin rằng, với sự chung tay của cộng đồng, các em nhỏ của trường Tiểu học Trung Lương sẽ có thêm nhiều động lực để vươn lên trong hành trình tương lai còn rất dài phía trước. Không những thế, có thêm trang thiết bị phục vụ công tác giảng dạy, các thầy cô như có thêm sự hỗ trợ cho mỗi bài giảng điện tử trở nên sinh động và cuốn hút hơn. Mỗi đóng góp của Quý vị dù ít hay nhiều cũng vô cùng quan trọng trong hành trình cổ vũ những mầm non tương lai của đất nước trong chặng đường học tập. Vì vậy hãy chung tay cùng chúng tôi để dự án sớm được triển khai bạn nhé!&nbsp;
</p>

<p>
	<u><strong>Về VIGE:</strong></u><br>
	Là tổ chức phi Chính phủ, hoạt động không vì lợi nhuận và được thành lập theo Quyết định số 2455/QĐ-BNV ngày 23/8/2017 của Bộ trưởng Bộ Nội vụ và được hoạt động trên phạm vi lãnh thổ Việt Nam. VIGEF nỗ lực hành động nhằm thúc đẩy đổi mới, sáng tạo và hiệu quả thông qua quá trình xã hội hóa giáo dục, tạo kết nối và trách nhiệm giữa các bên có liên quan hướng tới một nền giáo dục phổ thông bình đẳng, có chất lượng và hiệu quả.
</p>
</article>', N'https://homepage.momocdn.net/blogscontents/momo-upload-api-240312140548-638458491481113620.jpg', 25000000, CAST(N'2024-03-11T00:00:00.000' AS DateTime), CAST(N'2024-06-20T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[FundraisingProject] ([ProjectId], [Type], [Title], [Story], [Image], [TargetAmount], [StartDate], [EndDate], [Discontinued]) VALUES (6, 1, N'Chung tay gây quỹ giúp 244 hộ nghèo có vốn phát triển kinh tế năm 2024 (Đợt 2)', N'<h2 class="py-5 text-xl font-semibold">Câu chuyện</h2>
	<article class="soju__prose small"><p>
	<strong>Niềm vui của người mẹ nghèo</strong><br>
	<br>
	Gia đình chị Trang&nbsp; tại Đức Linh, Bình Thuận có 6 người. Chị Trang phải ở nhà chăm sóc cậu con trai út 2.5 tuổi mắc bệnh hen suyễn và người mẹ già mù lòa. Thu nhập của họ phụ thuộc vào chồng đi làm thuê hàng ngày và họ có 2.000m2 ruộng lúa. Thu nhập trung bình: 870.000 VNĐ/người/tháng. Cậu con trai út bị hen nặng nên chị phải ở nhà chăm sóc, không đi làm gì được. Mà việc đưa con đi khám ở bệnh viện tại Thành phố Hồ Chí Minh quá sức so với gia đình, vì còn phải lo cơm từng bữa.
</p>

<p>
	May mắn với đồng vốn được nhận từ MoMo năm 2023, chị có vốn để mở một quán nước nhỏ ở trước nhà, và mua thêm phân bón cho 2.000m2 ruộng lúa, giúp cải thiện thu nhập để cho con ăn học và lo cho mẹ già mù lòa. Bên cạnh đó Thiện Chí cũng hỗ trợ chi phí di chuyển, khám bệnh tại Thành phố Hồ Chí Minh và Trung tâm Thiện Chí cũng giới thiệu đúng bác sĩ chuyên khoa cho bé, sau đó sức khoẻ bé út đã khoẻ dần lên sau khi được chỉ định đúng thuốc, và đến hiện tại bé không phải đi tái khám hàng tháng.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240312135425-638458484651699879.jpg" style="width: 100%;">
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240312135440-638458484803837304.jpg" style="width: 100%;">
</p>

<p style="text-align: center;">
	<em>Nhiều gia đình nghèo khó đã nhận được nguồn vốn để có thể cải thiện đời sống kinh tế</em>
</p>

<p>
	Sau 17 năm làm việc với cộng đồng khó khăn, Trung tâm Thiện Chí hiểu rõ cộng đồng hơn bao giờ hết. Nhu cầu của hộ khó khăn chỉ từ 3 đến 5 triệu đồng. Sau khi hỗ trợ vốn, Trung tâm Thiện Chí sẽ tập huấn kiến thức cho hộ và thăm hộ hàng tháng để đảm bảo rằng hộ có đủ kỹ thuật trồng trọt chăn nuôi, và hạn chế tối đa rủi ro từ nguồn vốn vay. Vốn hỗ trợ cho bà con luôn luôn đảm bảo 0% lãi suất. Sự đồng hành từ Cộng đồng Heo Đất sẽ là món quà quý giá nhất cho cộng đồng nghèo trong năm mới 2024. Trong dự án này, Siêu ứng dụng MoMo sẽ kết hợp cùng Trung tâm Thiện Chí dự kiến sẽ hỗ trợ cho tổng cộng 244 hộ khó khăn và được chia thành 2 đợt gây quỹ. Đợt 1 đã được gây quỹ thành công trong tháng 01/2024 và tiến hành cấp vốn 122 hộ vào tháng 03/2024. Đợt 2 được gây quỹ trong tháng 02/2024 và cấp vốn 122 hộ vào tháng 04/2024.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240312135533-638458485333414519.jpg" style="width: 100%;">
</p>

<p style="text-align: center;">
	<em>Trung tâm Thiện Chí sẽ tập huấn kiến thức cho hộ và thăm hộ hàng tháng để đảm bảo rằng hộ có đủ kỹ thuật trồng trọt chăn nuôi, và hạn chế tối đa rủi ro từ nguồn vốn vay</em>
</p>

<p>
	<strong>Chung tay góp Heo Vàng giúp đỡ các hộ gia đình khó khăn cùng Trung tâm Thiện Chí</strong>
</p>

<p>
	Sau thành công của đợt gây quỹ lần 1 vào tháng 01/2024, Siêu ứng dụng MoMo và Trung tâm Thiện Chí mong muốn tiếp tục gây quỹ số tiền mặt là 115.000.000 đồng. Số tiền này sẽ được sử dụng để cung cấp vốn cho 122 hộ nghèo khó khăn. Sau 2 đợt gây quỹ, với số vốn vay thành công gây quỹ từ cộng đồng và số tiền được nhà tài trợ quy đổi từ Heo Vàng, 244 hộ khó khăn và hơn 400 trẻ em sẽ được hưởng lợi từ chương trình, các em sẽ có thể tiếp tục đến trường, bữa ăn đầy đủ dinh dưỡng hơn nhờ kinh tế gia đình được cải thiện.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240312135600-638458485604569031.jpg" style="width: 100%;">
</p>

<p style="text-align: center;">
	<em>Gây quỹ giúp những người dân nghèo có nguồn vốn phát triển kinh tế, ổn định cuộc sống</em>
</p>

<p>
	Mỗi quyên góp dù ít hay nhiều của Quý vị cũng sẽ giúp chúng tôi tiếp cận với mục tiêu của chương trình và cải thiện đời sống của nhiều gia đình khó khăn. Hãy cùng chúng tôi tạo ra sự khác biệt và giúp đỡ những người có hoàn cảnh khó khăn.
</p>

<p>
	<u><strong>Về Trung tâm Thiện Chí:</strong></u><br>
	Thiện Chí là một tổ chức Phi Chính phủ, hoạt động trong lĩnh vực phát triển cộng đồng từ năm 2005 tại Bình Thuận (Tánh Linh, Đức Linh, Hàm Thuận Nam). Các hoạt động của Thiện Chí 100% là hỗ trợ các hộ khó khăn phát triển kinh tế, học bổng cho các học sinh nghèo, tạo việc làm cho phụ nữ nghèo, các chương trình trường học như giới tính, vệ sinh răng miệng, môi trường.&nbsp;
</p>

<p>
	Mỗi năm Thiện Chí hỗ trợ cho hơn 2,000 hộ khó khăn phát triển kinh tế, hơn 1,500 học bổng cho các em học sinh cấp 1,2,3. Gần 100 người thất nghiệp có thu nhập trung bình hơn 2,000,000đ/tháng với những công việc part-time từ Thiện Chí, trung bình hơn 20,000 trẻ em, học sinh tiểu học, THCS có các kiến thức về vệ sinh răng miệng, giáo dục giới tính mỗi năm. Với kinh nghiệm làm việc với cộng đồng hơn 17 năm qua, chúng tôi và cộng đồng luôn là một. Và chúng tôi làm việc với một đội ngũ nhân sự kinh nghiệm và nhiệt huyết với cộng đồng khó khăn.
</p>
</article>', N'https://homepage.momocdn.net/blogscontents/momo-upload-api-240312135319-638458483997570319.jpg', 50000000, CAST(N'2024-02-20T00:00:00.000' AS DateTime), CAST(N'2024-08-13T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[FundraisingProject] ([ProjectId], [Type], [Title], [Story], [Image], [TargetAmount], [StartDate], [EndDate], [Discontinued]) VALUES (7, 2, N'Gây quỹ trao cơ hội cơ hội học tiếng anh, STEM, giáo dục khởi nghiệp cho các em học sinh vùng biên giới (Đợt 1)', N'<h2 class="py-5 text-xl font-semibold">Câu chuyện</h2>
	<article class="soju__prose small"><p>
	Theo báo cáo của Bộ Giáo dục và Đào tạo, cuối năm học 2022 - 2023, cả nước ta đang thiếu 118.253 giáo viên các cấp. Đến đầu năm học 2023 - 2024 này, số lượng giáo viên thiếu còn tăng lên nhiều hơn nữa. Cơ cấu đội ngũ nhà giáo đang mất cân đối giữa các môn học trong cùng cấp học, giữa các vùng miền. Tình trạng thiếu giáo viên ở các môn học mới như tiếng Anh, tin học… chậm được khắc phục, đặc biệt thừa giáo viên ở các thành phố lớn nhưng thiếu giáo viên trầm trọng ở miền núi, vùng sâu vùng xa. Bên cạnh đó, thực trạng thiếu thông tin và điều kiện kinh tế khó khăn ở vùng sâu, vùng xa ngày càng gây áp lực lên vai của các em, dẫn đến tình trạng trẻ em bỏ học dưới 18 tuổi để lao động kiếm thu nhập phụ giúp gia đình, thu hẹp cơ hội lựa chọn nghề nghiệp trong tương lai của trẻ.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240306125153-638453263130340465.jpg" style="width:100%">
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240306125200-638453263206580017.jpg" style="width:100%">
</p>

<p style="text-align:center">
	<em>Nhiều em học sinh ở miền núi, vùng sâu vùng xa chưa thực sự được tiếp cận với tiếng Anh và Tin học</em>
</p>

<p>
	Vì vậy, để mọi trẻ em Việt Nam được phát huy tối đa tiềm năng của bản thân thông qua cơ hội được học tập công bằng và chất lượng cần sự chung tay của cả cộng đồng để tạo ra những tác động tích cực cho nền giáo dục nước nhà.&nbsp;
</p>

<p>
	Giảng dạy vì Việt Nam đang nỗ lực giảm thiểu bất bình đẳng, thúc đẩy công bằng và nâng cao chất lượng giáo dục cho mọi trẻ em Việt Nam thông qua chương trình Phát triển Nhà giáo dục tiên phong. Chương trình tìm kiếm và phát triển các bạn trẻ đến từ nhiều ngành nghề, sau khi được đào tạo sẽ tham gia giảng dạy và thực hiện các dự án tại các trường công lập trong 2 năm.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240306125219-638453263392974541.jpg" style="width:100%">
</p>

<p style="text-align:center">
	<em>Chung tay cùng chúng tôi để giảm thiểu bất bình đẳng, thúc đẩy công bằng và nâng cao chất lượng giáo dục</em>
</p>

<p>
	Hơn bao giờ hết, Giảng dạy vì Việt Nam đang rất cần sự chung tay của bạn để cùng giúp 2 tỉnh Quảng Nam và Đồng Tháp tiếp tục kiến tạo nền giáo dục hoàn thiện cho hơn 4.000 học sinh thuộc 15 trường công lập vùng biên giới, khó khăn, thông qua phát triển các nhà giáo dục tiên phong để tham gia giảng dạy tiếng Anh, STEM, Giáo dục khởi nghiệp cho các bạn học sinh trong năm học 2023 - 2024.&nbsp;
</p>

<p>
	Dự án sẽ được tiến hành chia thành 3 đợt gây quỹ vào tháng 3, 4, 5/2024. Trong đợt gây quỹ lần 1 này, Siêu ứng dụng MoMo kết hợp cùng Giảng dạy vì Việt Nam kêu gọi sự đồng hành các nhà hảo tâm, các mạnh thường quân trong cả nước cùng chung tay quyên góp số tiền là 150.000.000 đồng. Ngoài ra, cũng trong đợt gây quỹ này, chúng tôi còn có 600 triệu đồng được các nhà tài trợ quy đổi từ chiến dịch quyên góp Heo Vàng. Số tiền gây quỹ sẽ được dùng để chi trả lương và phúc lợi cho các nhà giáo dục tiên phong, chi phí thực hiện các dự án cộng đồng tại địa phương và chi phí vận hành.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240306125244-638453263643042934.jpg" style="width:100%">
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240306125251-638453263718001190.jpg" style="width:100%">
</p>

<p style="text-align:center">
	<em>Chung tay cùng chương trình Phát triển Nhà giáo dục tiên phong đào tạo ra những giảng viên có thể giảng dạy tiếng Anh, STEM, Giáo dục khởi nghiệp tại các vùng khó khăn</em>
</p>

<p>
	Giáo dục là hành trình bền bỉ, đòi hỏi nhiều nỗ lực và thời gian để tạo ra sự thay đổi, vì vậy mỗi đóng góp dù ít hay nhiều của bạn cũng vô cùng đáng quý. Chúng tôi tin rằng, sự góp sức và chung tay của bạn sẽ cùng chúng tôi tạo ra thay đổi từ những điều đơn giản nhưng thiết thực trong mỗi ngày đến trường của các em học sinh vùng khó khăn. Cùng chung tay kiến tạo một nền giáo dục hoàn thiện cho mọi trẻ em Việt Nam bạn nhé!
</p>

<p>
	<u><strong>Về Giảng dạy vì Việt Nam - Teach For Viet Nam:</strong></u><br>
	Giảng dạy vì Việt Nam - Teach For Viet Nam (TFV) là doanh nghiệp xã hội phi lợi nhuận về giáo dục và là đối tác của mạng lưới giáo dục toàn cầu Teach For All.<br>
	TFV hoạt động với sứ mệnh tìm kiếm và phát triển mạng lưới các nhà giáo dục tiên phong, những người sẽ giảng dạy và đồng kiến tạo nên một nền giáo dục công bằng và chất lượng cho mọi trẻ em Việt Nam. Sau 7 năm hoạt động, TFV đã phát triển 82 Nhà giáo dục tiên phong, tham gia giảng dạy tại 111 trường thuộc các tỉnh Tây Ninh, Quảng Nam và Đồng Tháp các môn tiếng Anh, STEM, giáo dục khởi nghiệp, tạo tác động tích cực đến hơn 36.000+ học sinh, 3000+ giáo viên, 2000+ phụ huynh.
</p>
</article>', N'https://homepage.momocdn.net/blogscontents/momo-upload-api-240306125112-638453262722027788.jpg', 60000000, CAST(N'2024-03-12T00:00:00.000' AS DateTime), CAST(N'2024-08-12T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[FundraisingProject] ([ProjectId], [Type], [Title], [Story], [Image], [TargetAmount], [StartDate], [EndDate], [Discontinued]) VALUES (8, 1, N'Phát tâm xây trường dịp năm mới 2024: Cùng xây phòng học mới cho thầy và trò tại Trường mầm non và tiểu học Lũng Vầy, Cao Bằng', N'<h2 class="py-5 text-xl font-semibold">Câu chuyện</h2>
	<article class="soju__prose small"><p>
	Chỉ cách thành phố Cao Bằng gần 150km, nhưng để đến được Trường PTDTBT mầm non và tiểu học Lũng Vầy thuộc xóm Phiêng Sáng, xã Cô Ba, vùng 3 huyện Bảo Lạc, tỉnh Cao Bằng bằng ô tô cũng phải mất 5 - 6 giờ do đường đi chủ yếu qua vùng đồi núi, vực sâu, dốc cao… Ai đến Lũng Vầy cũng dễ dàng cảm nhận thời tiết khắc nghiệt ở vùng đất này, trời đang nắng nhưng bất chợt một trận sương mù, mưa phùn lại kéo xuống...
</p>

<p>
	Trường PTDTBT mầm non và tiểu học Lũng Vầy là nơi theo học của 546 học sinh với 5 điểm trường nhỏ. Nhiều thầy cô giáo và học trò ở trường chia sẻ rằng, gần như năm nào mùa đông về cũng lạnh giá đến đóng băng, mùa hè thì mưa đá và gió lốc. Người dân trong xã Cô Ba chủ yếu là đồng bào dân tộc người Mông và Dao. Cuộc sống của họ dựa vào một ít diện tích cây sao mộc và lúa trên nương nên còn nhiều khó khăn. Đời sống nhân dân bấp bênh nên việc học của con trẻ cũng chưa thực sự được quan tâm. Nhiều lúc học sinh đến lớp quần áo không đủ mặc, nhất là vào mùa đông phải chịu đựng cái lạnh tê tái của gió mùa.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240228151422-638447300623477447.jpg" style="width:100%">
</p>

<p style="text-align:center">
	<em>Cuộc sống của người dân còn nhiều khó khăn xã Cô Ba còn rất nhiều khó khăn nên bữa cơm của các em nhỏ cũng đạm bạc</em>
</p>

<p>
	Điểm trường Lũng Vầy thuộc Trường PTDTBT mầm non và tiểu học Lũng Vầy, nằm ở Xóm Lũng Vầy, xã Cô Ba, huyện Bảo Lạc, tỉnh Cao Bằng với 5 lớp từ lớp 1 đến lớp 5 bao gồm: 15 học sinh lớp 1; 15 học sinh lớp 2; 16 học sinh lớp 3, 12 học sinh lớp 4 và 9 học sinh lớp 5. Các em đều thuộc đồng bào dân tộc thiểu số người Mông, Dao và Nùng. Hiện nay trường có 3 phòng lớp học đơn sơ chỉ được dựng bằng mái tôn từ năm 2013. Những ngày mưa lớn âm thanh vô cùng ồn ào, những ngày nắng phòng học lại vô cùng oi bức. Trải qua thời gian sử dụng, những tấm tôn trên mái còn bị cong vênh không còn che được nắng mưa, thậm chí nếu có bão còn có thể gây nguy hiểm cho các em nhỏ và thầy cô nơi đây. Hơn thế nữa, trường còn thiếu 2 phòng học và 1 lớp tin học, vì vậy các em học sinh lớp 4 và lớp 5 hiện đang gửi đi học chỗ khác cách trường 3km.
</p>

<p>
	Vượt qua muôn vàn khó khăn, thiếu thốn, nhiều thầy cô giáo vẫn bám trụ lại đây, đồng hành cùng các em nhỏ từ các bản làng xa xôi đến trường với hy vọng thắp sáng ước mơ con chữ. Còn nhiều khó khăn, thiếu thốn nhưng niềm vui mỗi ngày của các thầy cô nơi đây được nhân đôi khi không có học sinh nào bỏ học. Chính điều này đã tăng thêm ý chí yêu nghề, mến trẻ, để các thầy cô giáo nơi vùng cao quyết tâm hơn trong việc gieo chữ giữa núi cao đại ngàn.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240228151500-638447301003951475.jpg" style="width:100%">
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240228151515-638447301158875879.jpg" style="width:100%">
</p>

<p style="text-align:center">
	<em>Thiếu thốn cơ sở vật chất nhưng tiếng đọc bài ê a vẫn vang vọng trên đại ngàn</em>
</p>

<p>
	Trên hành trình mang tấm lòng của các nhà hảo tâm đến những điểm trường xa xôi nơi vùng núi phía Bắc, chúng tôi cảm nhận được niềm hạnh phúc sâu sắc khi có thể thắp lên niềm vui, niềm tin và động lực học tập cho các em. Vì vậy chương trình “Xây Trường Cho Em 2024: Tiếp Bước Tương Lai" ra đời nhằm hướng đến mục tiêu “tạo ra giá trị bền vững cho tương lai của các em nhỏ vùng cao". Bởi vì, phải tận mắt chứng kiến hoàn cảnh và điều kiện học tập còn muôn vàn thiệt thòi của các em, chúng tôi mới nhận ra chỉ một ngày vui là không đủ.
</p>

<p>
	Chúng tôi thấu hiểu một phòng học chắc chắn, có thể che mưa, chắn gió là cần thiết và có ý nghĩa thế nào với việc học tập của các em. Bởi không chỉ là nơi học tập, trường học với nhiều em còn là mái nhà thứ hai - nơi ăn uống, sinh hoạt, nơi phụ huynh yên tâm gửi gắm các em cho nhà trường… Từ đó, Siêu ứng dụng MoMo kết hợp cùng Giáo hội Phật giáo Việt Nam, Truyền hình Bchannel - BTV9 An Viên cùng kêu gọi cộng đồng chung tay mang đến phòng học mới cho các em học sinh và thầy cô tại Trường mầm non và tiểu học Lũng Vầy. Để chương trình từ thiện “Xây Trường Cho Em 2024: Tiếp Bước Tương Lai” sớm được triển khai, từ ngày 01.02.2024 đến hết ngày 30.09.2024, chương trình rất mong nhận được sự đóng góp hoan hỷ của các Quý Phật tử cùng chung tay quyên góp số tiền là 400.000.000đ. Số tiền sẽ được sử dụng để xây dựng 1 phòng học với diện tích tiêu chuẩn khoảng 38 - 40m2.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240228151548-638447301489489857.jpg" style="width:100%">
</p>

<p style="text-align:center">
	<em>Chung tay gây quỹ trao tặng phòng học mới cho thầy và trò Trường mầm non và tiểu học Lũng Vầy</em>
</p>

<p>
	Phòng học mới dành cho thầy cô ở Trường mầm non và tiểu học Lũng Vầy không chỉ mang ý nghĩa về mặt kinh tế, mà còn là yếu tố giúp đồng bào ở vùng núi, đặc biệt là những nơi giáp biên yên tâm, ổn định phát triển kinh tế cũng như các vấn đề giáo dục, văn hóa. Vì vậy dự án của chúng tôi rất cần sự chung tay góp sức của cộng đồng các nhà hảo tâm các mạnh thường quân trong cả nước.
</p>

<p>
	<u><strong>Về Giáo Hội Phật Giáo Việt Nam:</strong></u><br>
	Là tổ chức Phật giáo toàn quốc của Việt Nam, là đại diện Tăng Ni, Phật tử Việt Nam trong và ngoài nước, là thành viên các tổ chức Phật giáo Quốc tế mà Giáo hội tham gia và là thành viên của Mặt trận Tổ Quốc Việt Nam. Giáo hội được thành lập sau Hội nghị thống nhất Phật Giáo Việt Nam tổ chức tại chùa Quán Sứ, Hà Nội, được triệu tập bởi Ban Vận Động Thống Nhất Phật giáo Việt Nam vào ngày 7 tháng 11 năm 1981 nhằm thống nhất tất cả các sinh hoạt Phật giáo của Tăng Ni, Phật tử Việt Nam
</p>
<em>*MoMo biết rằng còn rất nhiều hoàn cảnh khó khăn trên khắp đất nước của chúng ta cần được bảo trợ. Bạn hay các công ty hãy liên hệ với chúng tôi để cùng tài trợ, giúp đỡ&nbsp; tạo nên một cộng đồng Việt Nam nhân ái nhé! donation@mservice.com.vn</em></article>', N'https://homepage.momocdn.net/blogscontents/momo-upload-api-240228151342-638447300224018834.jpg', 70000000, CAST(N'2024-01-12T00:00:00.000' AS DateTime), CAST(N'2024-05-12T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[FundraisingProject] ([ProjectId], [Type], [Title], [Story], [Image], [TargetAmount], [StartDate], [EndDate], [Discontinued]) VALUES (9, 1, N'Phát tâm xây trường dịp năm mới 2024: Cùng xây phòng học mới cho thầy và trò tại Trường mầm non và tiểu học Lũng Vầy, Cao Bằng', N'<h2 class="py-5 text-xl font-semibold">Câu chuyện</h2>
	<article class="soju__prose small"><p>
	Chỉ cách thành phố Cao Bằng gần 150km, nhưng để đến được Trường PTDTBT mầm non và tiểu học Lũng Vầy thuộc xóm Phiêng Sáng, xã Cô Ba, vùng 3 huyện Bảo Lạc, tỉnh Cao Bằng bằng ô tô cũng phải mất 5 - 6 giờ do đường đi chủ yếu qua vùng đồi núi, vực sâu, dốc cao… Ai đến Lũng Vầy cũng dễ dàng cảm nhận thời tiết khắc nghiệt ở vùng đất này, trời đang nắng nhưng bất chợt một trận sương mù, mưa phùn lại kéo xuống...
</p>

<p>
	Trường PTDTBT mầm non và tiểu học Lũng Vầy là nơi theo học của 546 học sinh với 5 điểm trường nhỏ. Nhiều thầy cô giáo và học trò ở trường chia sẻ rằng, gần như năm nào mùa đông về cũng lạnh giá đến đóng băng, mùa hè thì mưa đá và gió lốc. Người dân trong xã Cô Ba chủ yếu là đồng bào dân tộc người Mông và Dao. Cuộc sống của họ dựa vào một ít diện tích cây sao mộc và lúa trên nương nên còn nhiều khó khăn. Đời sống nhân dân bấp bênh nên việc học của con trẻ cũng chưa thực sự được quan tâm. Nhiều lúc học sinh đến lớp quần áo không đủ mặc, nhất là vào mùa đông phải chịu đựng cái lạnh tê tái của gió mùa.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240228151422-638447300623477447.jpg" style="width:100%">
</p>

<p style="text-align:center">
	<em>Cuộc sống của người dân còn nhiều khó khăn xã Cô Ba còn rất nhiều khó khăn nên bữa cơm của các em nhỏ cũng đạm bạc</em>
</p>

<p>
	Điểm trường Lũng Vầy thuộc Trường PTDTBT mầm non và tiểu học Lũng Vầy, nằm ở Xóm Lũng Vầy, xã Cô Ba, huyện Bảo Lạc, tỉnh Cao Bằng với 5 lớp từ lớp 1 đến lớp 5 bao gồm: 15 học sinh lớp 1; 15 học sinh lớp 2; 16 học sinh lớp 3, 12 học sinh lớp 4 và 9 học sinh lớp 5. Các em đều thuộc đồng bào dân tộc thiểu số người Mông, Dao và Nùng. Hiện nay trường có 3 phòng lớp học đơn sơ chỉ được dựng bằng mái tôn từ năm 2013. Những ngày mưa lớn âm thanh vô cùng ồn ào, những ngày nắng phòng học lại vô cùng oi bức. Trải qua thời gian sử dụng, những tấm tôn trên mái còn bị cong vênh không còn che được nắng mưa, thậm chí nếu có bão còn có thể gây nguy hiểm cho các em nhỏ và thầy cô nơi đây. Hơn thế nữa, trường còn thiếu 2 phòng học và 1 lớp tin học, vì vậy các em học sinh lớp 4 và lớp 5 hiện đang gửi đi học chỗ khác cách trường 3km.
</p>

<p>
	Vượt qua muôn vàn khó khăn, thiếu thốn, nhiều thầy cô giáo vẫn bám trụ lại đây, đồng hành cùng các em nhỏ từ các bản làng xa xôi đến trường với hy vọng thắp sáng ước mơ con chữ. Còn nhiều khó khăn, thiếu thốn nhưng niềm vui mỗi ngày của các thầy cô nơi đây được nhân đôi khi không có học sinh nào bỏ học. Chính điều này đã tăng thêm ý chí yêu nghề, mến trẻ, để các thầy cô giáo nơi vùng cao quyết tâm hơn trong việc gieo chữ giữa núi cao đại ngàn.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240228151500-638447301003951475.jpg" style="width:100%">
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240228151515-638447301158875879.jpg" style="width:100%">
</p>

<p style="text-align:center">
	<em>Thiếu thốn cơ sở vật chất nhưng tiếng đọc bài ê a vẫn vang vọng trên đại ngàn</em>
</p>

<p>
	Trên hành trình mang tấm lòng của các nhà hảo tâm đến những điểm trường xa xôi nơi vùng núi phía Bắc, chúng tôi cảm nhận được niềm hạnh phúc sâu sắc khi có thể thắp lên niềm vui, niềm tin và động lực học tập cho các em. Vì vậy chương trình “Xây Trường Cho Em 2024: Tiếp Bước Tương Lai" ra đời nhằm hướng đến mục tiêu “tạo ra giá trị bền vững cho tương lai của các em nhỏ vùng cao". Bởi vì, phải tận mắt chứng kiến hoàn cảnh và điều kiện học tập còn muôn vàn thiệt thòi của các em, chúng tôi mới nhận ra chỉ một ngày vui là không đủ.
</p>

<p>
	Chúng tôi thấu hiểu một phòng học chắc chắn, có thể che mưa, chắn gió là cần thiết và có ý nghĩa thế nào với việc học tập của các em. Bởi không chỉ là nơi học tập, trường học với nhiều em còn là mái nhà thứ hai - nơi ăn uống, sinh hoạt, nơi phụ huynh yên tâm gửi gắm các em cho nhà trường… Từ đó, Siêu ứng dụng MoMo kết hợp cùng Giáo hội Phật giáo Việt Nam, Truyền hình Bchannel - BTV9 An Viên cùng kêu gọi cộng đồng chung tay mang đến phòng học mới cho các em học sinh và thầy cô tại Trường mầm non và tiểu học Lũng Vầy. Để chương trình từ thiện “Xây Trường Cho Em 2024: Tiếp Bước Tương Lai” sớm được triển khai, từ ngày 01.02.2024 đến hết ngày 30.09.2024, chương trình rất mong nhận được sự đóng góp hoan hỷ của các Quý Phật tử cùng chung tay quyên góp số tiền là 400.000.000đ. Số tiền sẽ được sử dụng để xây dựng 1 phòng học với diện tích tiêu chuẩn khoảng 38 - 40m2.
</p>

<p>
	<img alt="" src="https://homepage.momocdn.net/blogscontents/momo-upload-api-240228151548-638447301489489857.jpg" style="width:100%">
</p>

<p style="text-align:center">
	<em>Chung tay gây quỹ trao tặng phòng học mới cho thầy và trò Trường mầm non và tiểu học Lũng Vầy</em>
</p>

<p>
	Phòng học mới dành cho thầy cô ở Trường mầm non và tiểu học Lũng Vầy không chỉ mang ý nghĩa về mặt kinh tế, mà còn là yếu tố giúp đồng bào ở vùng núi, đặc biệt là những nơi giáp biên yên tâm, ổn định phát triển kinh tế cũng như các vấn đề giáo dục, văn hóa. Vì vậy dự án của chúng tôi rất cần sự chung tay góp sức của cộng đồng các nhà hảo tâm các mạnh thường quân trong cả nước.
</p>

<p>
	<u><strong>Về Giáo Hội Phật Giáo Việt Nam:</strong></u><br>
	Là tổ chức Phật giáo toàn quốc của Việt Nam, là đại diện Tăng Ni, Phật tử Việt Nam trong và ngoài nước, là thành viên các tổ chức Phật giáo Quốc tế mà Giáo hội tham gia và là thành viên của Mặt trận Tổ Quốc Việt Nam. Giáo hội được thành lập sau Hội nghị thống nhất Phật Giáo Việt Nam tổ chức tại chùa Quán Sứ, Hà Nội, được triệu tập bởi Ban Vận Động Thống Nhất Phật giáo Việt Nam vào ngày 7 tháng 11 năm 1981 nhằm thống nhất tất cả các sinh hoạt Phật giáo của Tăng Ni, Phật tử Việt Nam
</p>
<em>*MoMo biết rằng còn rất nhiều hoàn cảnh khó khăn trên khắp đất nước của chúng ta cần được bảo trợ. Bạn hay các công ty hãy liên hệ với chúng tôi để cùng tài trợ, giúp đỡ&nbsp; tạo nên một cộng đồng Việt Nam nhân ái nhé! donation@mservice.com.vn</em></article>', N'https://homepage.momocdn.net/blogscontents/momo-upload-api-240228151342-638447300224018834.jpg', 70000000, CAST(N'2024-01-12T00:00:00.000' AS DateTime), CAST(N'2024-05-12T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[FundraisingProject] OFF
GO
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638460832488370123', 3, N'MOMOQR', NULL, N'23000', N'nam ung ho', N'1', N'Đang xử lí', CAST(N'2023-12-30T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638460832488370592', 4, N'MomoQR', NULL, N'12000', N'Trường Vũ Vũvtv ck', N'0', N'Thành công', CAST(N'2024-01-30T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638460833043916532', 5, N'MomoQR', NULL, N'12000', N'Trường Vũ Vũvtv ck', N'0', N'Thành công', CAST(N'2024-02-13T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638460833287875123', 6, N'VNPAY', N'TPB', N'120000', N'vtv ck', N'1', N'Đang xử lí', CAST(N'2024-03-13T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638460833287875234', 7, N'VNPAY', N'VCB', N'10000', N'manh ck', N'1', N'Đang xử lí', CAST(N'2024-03-13T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638460833287875304', 8, N'MomoQR', NULL, N'12000', N'Trường Vũ Vũvtv ck', N'0', N'Thành công', CAST(N'2024-03-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638460833287875345', 9, N'VNPAY', N'AGB', N'34000', N'tuan ck', N'1', N'Đang xử lí', CAST(N'2024-03-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638466738891786547', 8, N'MomoQR', NULL, N'100000', N'Vũ Trường VũVTV ủng hộ gia đình khó khăn', N'0', N'Thành công', CAST(N'2024-03-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638466746627578639', 7, N'MomoQR', NULL, N'50000', N'Vũ Trường Vũhoang minh hung ung ho', N'0', N'Thành công', CAST(N'2024-03-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638467008113737720', 6, N'VnPay', N'NCB', N'40000', N'Đỗ Quang Anh một nắm khi đói bằng một gói khi no 40000 ', N'00', N'Thành Công', CAST(N'2024-03-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638467009961209327', 5, N'VnPay', N'NCB', N'20000', N'Thắng ligo quyen gop 20000 ', N'00', N'Thành Công', CAST(N'2024-03-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638467011980160439', 4, N'VnPay', N'NCB', N'40000', N'Thắng ligo ligo Dinh ngu 40000 ', N'00', N'Thành Công', CAST(N'2024-03-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([OrderId], [ProjectId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfDonation]) VALUES (N'638467246569639044', 9, N'MomoQR', NULL, N'50000', N'Vũ Trường Vũủng hộ xây trường cho các em', N'0', N'Thành công', CAST(N'2024-03-23T00:18:28.597' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName], [Description]) VALUES (1, N'ADMIN', N'admin')
INSERT [dbo].[Role] ([RoleId], [RoleName], [Description]) VALUES (2, N'STAFF', N'staff')
INSERT [dbo].[Role] ([RoleId], [RoleName], [Description]) VALUES (3, N'LECTURER', N'organization')
INSERT [dbo].[Role] ([RoleId], [RoleName], [Description]) VALUES (5, N'STUDENT', N'client')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active]) VALUES (15, N'admin', N'userFirstName', N'userLastName', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'vu', N'0329053999', N'HN', 1)
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active]) VALUES (25, N'lecturer', N'name', N'name', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'name', N'0329033545', N'HN', 1)
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active]) VALUES (26, N'staff', N'name', N'name', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'email', N'3548293525', N'HN', 1)
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active]) VALUES (27, N'student1', N'name', N'name', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'email', N'9572985792', N'HN', 1)
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active]) VALUES (29, N'student2', N'name', N'name', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'email', N'5374567454', N'HN', 1)
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active]) VALUES (30, N'student3', N'name', N'name', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'email', N'5374567454', N'HN', 1)
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active]) VALUES (31, N'student4', N'name', N'name', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'email', N'5374567454', N'HN', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (15, 15, 1)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (16, 25, 3)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (17, 26, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (20, 27, 5)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (28, 29, 5)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (29, 30, 5)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (30, 31, 5)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
SET IDENTITY_INSERT [dbo].[CourseEnroll] ON 

INSERT [dbo].[CourseEnroll] ([CourseEnrollID], [UserID], [CourseID], [EnrollDate], [LessonCurrent], [CourseStatus], [Grade], [AverageGrade], [StudentFeeId]) VALUES (2, 27, 2, CAST(N'2024-06-20' AS Date), 7, 0, NULL, NULL, NULL)
INSERT [dbo].[CourseEnroll] ([CourseEnrollID], [UserID], [CourseID], [EnrollDate], [LessonCurrent], [CourseStatus], [Grade], [AverageGrade], [StudentFeeId]) VALUES (3, 27, 2, CAST(N'2024-06-11' AS Date), 1, 1, NULL, NULL, N'638537160457784265')
INSERT [dbo].[CourseEnroll] ([CourseEnrollID], [UserID], [CourseID], [EnrollDate], [LessonCurrent], [CourseStatus], [Grade], [AverageGrade], [StudentFeeId]) VALUES (7, 27, 2, CAST(N'2024-06-11' AS Date), 1, 1, NULL, NULL, N'638536924482397839')
INSERT [dbo].[CourseEnroll] ([CourseEnrollID], [UserID], [CourseID], [EnrollDate], [LessonCurrent], [CourseStatus], [Grade], [AverageGrade], [StudentFeeId]) VALUES (8, 27, 12, CAST(N'2024-06-11' AS Date), 1, 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CourseEnroll] OFF
GO
SET IDENTITY_INSERT [dbo].[Lesson] ON 

INSERT [dbo].[Lesson] ([LessonID], [LessonNum], [CourseID], [Name], [Description], [VideoUrl], [Quiz], [PreviousLessioNum]) VALUES (4, 1, 2, N'Giới thiệu khóa học', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'Az9GyMlb8S0', N'[
  {
    "questionNo": 1,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 2,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 3,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 4,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 5,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  }
]', 0)
INSERT [dbo].[Lesson] ([LessonID], [LessonNum], [CourseID], [Name], [Description], [VideoUrl], [Quiz], [PreviousLessioNum]) VALUES (5, 2, 2, N'Cài đặt SqlServer', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'F8JaV_QbgTg', N'[
  {
    "questionNo": 1,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 2,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 3,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 4,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 5,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  }
]', 1)
INSERT [dbo].[Lesson] ([LessonID], [LessonNum], [CourseID], [Name], [Description], [VideoUrl], [Quiz], [PreviousLessioNum]) VALUES (6, 3, 2, N'Create/Drop Database', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'F8JaV_QbgTg', N'[
  {
    "questionNo": 1,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 2,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 3,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 4,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 5,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  }
]', 2)
INSERT [dbo].[Lesson] ([LessonID], [LessonNum], [CourseID], [Name], [Description], [VideoUrl], [Quiz], [PreviousLessioNum]) VALUES (7, 4, 2, N'Câu lệnh Select', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'Az9GyMlb8S0', N'[
  {
    "questionNo": 1,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 2,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 3,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 4,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 5,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  }
]', 3)
INSERT [dbo].[Lesson] ([LessonID], [LessonNum], [CourseID], [Name], [Description], [VideoUrl], [Quiz], [PreviousLessioNum]) VALUES (8, 5, 2, N'Điều kiện Where', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'Az9GyMlb8S0', N'[
  {
    "questionNo": 1,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 2,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 3,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 4,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 5,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  }
]', 4)
INSERT [dbo].[Lesson] ([LessonID], [LessonNum], [CourseID], [Name], [Description], [VideoUrl], [Quiz], [PreviousLessioNum]) VALUES (9, 6, 2, N'Điều kiện Where', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'Az9GyMlb8S0', N'[
  {
    "questionNo": 1,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 2,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 3,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 4,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 5,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  }
]', 5)
INSERT [dbo].[Lesson] ([LessonID], [LessonNum], [CourseID], [Name], [Description], [VideoUrl], [Quiz], [PreviousLessioNum]) VALUES (10, 1, 3, N'Giới thiệu khóa học ', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'Az9GyMlb8S0', N'[
  {
    "questionNo": 1,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 2,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 3,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 4,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  },
  {
    "questionNo": 5,
    "question": "Lorem ahduifghise suidgfhgdfghk sidfgs sryut?",
    "answerA": "dsrtiu sdrtiguhi ",
    "answerB": "sỉghis drgidh ",
    "answerC": "drigdirg druigyh ",
    "answerD": "sdirguh áotu sỉty ",
    "answer": "A",
    "correctAnswer": "D"
  }
]', 0)
SET IDENTITY_INSERT [dbo].[Lesson] OFF
GO
INSERT [dbo].[StudentFee] ([StudentFeeId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfPaid], [CourseEnrollId]) VALUES (N'638536910922383172', N'MomoQR', NULL, N'299000', N'Vũ Trường Vũthanh toán tiền học', N'0', N'Thành công', CAST(N'2024-06-11T15:33:50.133' AS DateTime), NULL)
INSERT [dbo].[StudentFee] ([StudentFeeId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfPaid], [CourseEnrollId]) VALUES (N'638536918245371873', N'MomoQR', NULL, N'299000', N'vuvuvtv thanh toan', N'0', N'Thành công', CAST(N'2024-06-11T15:37:31.970' AS DateTime), NULL)
INSERT [dbo].[StudentFee] ([StudentFeeId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfPaid], [CourseEnrollId]) VALUES (N'638536922563101814', N'MomoQR', NULL, N'11111', N'Vũ Trường Vũck', N'0', N'Thành công', CAST(N'2024-06-11T15:44:43.953' AS DateTime), NULL)
INSERT [dbo].[StudentFee] ([StudentFeeId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfPaid], [CourseEnrollId]) VALUES (N'638536923728011732', N'MomoQR', NULL, N'11111', N'Vũ Trường Vũvtv ck', N'0', N'Thành công', CAST(N'2024-06-11T15:46:29.223' AS DateTime), NULL)
INSERT [dbo].[StudentFee] ([StudentFeeId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfPaid], [CourseEnrollId]) VALUES (N'638536924482397839', N'MomoQR', NULL, N'11111', N'Vũ Trường Vũctc ck', N'0', N'Thành công', CAST(N'2024-06-11T15:47:55.130' AS DateTime), NULL)
INSERT [dbo].[StudentFee] ([StudentFeeId], [PaymentMethod], [BankCode], [Amount], [OrderInfo], [ErrorCode], [LocalMessage], [DateOfPaid], [CourseEnrollId]) VALUES (N'638537160457784265', N'VnPay', N'NCB', N'100000', N'Vũ Trường Vũ VuTruongVu thanh toan tien hoc 100000 ', N'00', N'Thành Công', CAST(N'2024-06-11T15:21:36.660' AS DateTime), NULL)
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240519145122_dbcontextver1', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240519153731_dbcontextver2', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240519154511_dbcontextver3', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240519155206_dbcontextver4', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240519161901_ver1', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240519162302_versin1', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240519162510_versin2', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240519162844_versin3', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240529173810_dbver5', N'6.0.29')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240609071346_dbcontextver5', N'6.0.29')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240609074815_dbcontextver6', N'6.0.29')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240609080546_dbcontextver7', N'6.0.29')
GO
