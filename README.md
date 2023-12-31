# Phần mềm tiếng Anh đa nền tảng
* Đồ án môn học: Lập trình trực quan.
* Học kì I năm học 2023-2024.
* Lớp: IT008.O12.
* Ngô Hương Giang - 22520357.
* Nguyễn Xuân Quang - 22521205.
* Trần Nguyễn Chí Huy - 22520577. 
* GVHD: ThS. Huỳnh Tuấn Anh.
## Giới thiệu
* Hiện nay, tiếng Anh đóng vai trò cực kì quan trọng trong giao tiếp quốc tế, trao đổi văn hóa và phát triển cá nhân. Với việc trở thành ngôn ngữ toàn cầu, tiếng Anh không chỉ là một công cụ giao tiếp mà còn là một yếu tố quan trọng hỗ trợ nhiều lĩnh vực khác nhau. Và đặc biệt, trong giáo dục, tiếng Anh không chỉ là một môn học quan trọng mà còn là công cụ để tiếp cận và nắm bắt kiến thức mới từ các nguồn thông tin quốc tế.
* Việc ứng dụng công nghệ thông tin vào việc học tiếng Anh đã ra đời từ rất lâu và được hưởng ứng mạnh mẽ trong những năm gần đây. Các phần mềm hay các website học trực tuyến tiếng Anh có mặt trên thị trường ngày càng nhiều, nhằm nâng cao khả năng hỗ trợ học tập cũng như giúp mọi người có nhiều sự lựa chọn phù hợp hơn cho bản thân. Khi viết một đoạn văn hay một bài luận, các từ đồng nghĩa, trái nghĩa sẽ giúp câu từ trở nên trôi chảy và mượt mà hơn, hạn chế được tối đa việc sử dụng một từ vựng quá nhiều lần (lỗi lặp từ) gây ra sự nhàm chán cho người đọc. Hơn nữa, khi chúng ta tập luyện sử dụng các từ đồng nghĩa, trái nghĩa, vốn từ vựng của chúng ta sẽ tăng lên. Điều này không những có thể giúp mọi người đạt được điểm số cao hơn trong các kì thi mà còn gây được ấn tượng tốt với mọi người xung quanh, từ đó các mối quan hệ và công việc cũng trở nên thuận lợi.
* Vì vậy, từ những yêu cầu và khảo sát từ thực tế, nhóm chúng em đã quyết định xây dựng phần mềm <b>ASDict</b> với giao diện gần gũi, tiện lợi, dễ sử dụng nhằm giúp người dùng có thêm lựa chọn cho bản thân.
## Các chức năng chính
* Cho phép người dùng nhập vào một từ.
* Đề xuất từ trong quá trình nhập.
* Tìm kiếm từ và hiển thị từ đồng nghĩa, trái nghĩa.
* Phân loại và hiển thị kết quả theo nhóm Synonym và Antonym.
* Gợi ý từ vựng cho người dùng.
* Hệ thống lưu từ vựng theo ý muốn người dùng (Bookmark).
* Sắp xếp Bookmark theo thứ tự bảng chữ cái Alphabet hoặc từ lưu trữ mới nhất.
* Lưu lịch sử tìm kiếm.
## Công nghệ
* Thiết kế giao diện: Figma
* Ngôn ngữ lập trình: C#
* Framework: .NET MAUI
* Kiến trúc: MVVM
## Giao diện ứng dụng
### Android
<ol>
<li style="text-align: center;">
  <span style="display: block;"><b>Splash Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/ec7add14-d9c3-43ec-aeab-446e37729802" width="200" height="450" style="display: inline-block;"/>
</li>
  <li style="text-align: center;">
  <span style="display: block;"><b>Home Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/9bfab05f-ac9b-42d1-8286-42b6307506c1" width="200" height="450" style="display: inline-block;"/>
</li>
  <li style="text-align: center;">
  <span style="display: block;"><b>Menu Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/13145639-0eca-435d-9168-cdb19ff57bb3" width="200" height="450" style="display: inline-block;"/>
</li>
  <li style="text-align: center;">
  <span style="display: block;"><b>Suggest Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/a5196b16-0bf9-409a-9797-cca652f7414a" width="200" height="450" style="display: inline-block;"/>
</li>
  <li style="text-align: center;">
  <span style="display: block;"><b>Synonym Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/2830de83-312f-483b-9b68-7da104392c67" width="200" height="450" style="display: inline-block;"/>
</li>
  </li>
  <li style="text-align: center;">
  <span style="display: block;"><b>Antonym Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/d3c9f840-2dfa-4913-8759-3ca5a6e1d659" width="200" height="450" style="display: inline-block;"/>
</li>
  </li>
  <li style="text-align: center;">
  <span style="display: block;"><b>Bookmark Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/4ff2c068-1f3d-4031-bb5b-631671acc652" width="200" height="450" style="display: inline-block;"/>
</li>
</ol>

### Window
<ol>
  <li style="text-align: center;">
  <span style="display: block;"><b>Home Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/8a6b7605-3fde-4ab8-85f4-70a7d30af907" style="display: inline-block;"/>
</li>
  <li style="text-align: center;">
  <span style="display: block;"><b>Synonym Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/30d83f96-cf70-476d-b91e-49c8ce6d2fc0" style="display: inline-block;"/>
</li>
  </li>
  <li style="text-align: center;">
  <span style="display: block;"><b>Antonym Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/14c0a0e5-677e-43ab-a614-0d3da0200457" style="display: inline-block;"/>
</li>
  </li>
  <li style="text-align: center;">
  <span style="display: block;"><b>Bookmark Screen</b><br></span>
  <img src="https://github.com/Tohew/ASDict/assets/127734828/c3c55984-d613-4b5e-8f6b-77a1b118c17a" style="display: inline-block;"/>
</li>
</ol>

## Hướng phát triển
* Chỉnh sửa giao diện người dùng ở bảng kết quả.
* Gọi nhiều API: hình ảnh minh họa, dịch Anh ↔ Việt, giọng nói, …
* Có khả năng viết lại đoạn văn mới bằng cách sử dụng các từ đồng nghĩa / trái nghĩa.
* Tạo bong bóng để hiển thị trên các ứng dụng khác, giúp thuận tiện cho việc tra cứu.Kết hợp máy ảnh để dịch Anh ↔ Việt hoặc viết lại đoạn văn.

## Link tải ứng dụng:
https://nghgi.github.io/ASDict-Download/
