create database WEBHOTEL
go
use WEBHOTEL
go
create table ROOMTYPE(
ID int primary key identity,
Name nvarchar(200),
Price float,
Sumary nvarchar(200),
Des nvarchar(max))
go
create table ACCOUNT(
Username varchar(50) primary key,
Password varchar(100),
Email varchar(100) unique,
Name nvarchar(100),
Role varchar(20))
go
create table CUSTOMER(
ID varchar(15) primary key,
Name nvarchar(50),
Phone nvarchar(10),
Email nvarchar(100),
Address nvarchar(100))
go
create table ROOM(
ID int primary key identity,
TypeID int,
foreign key (TypeID) references ROOMTYPE(ID),
Status bit)
go
create table RESERVATION(
ID int primary key identity,
CustomerID varchar(15),
foreign key (CustomerID) references CUSTOMER(ID),
CheckIn date,
CheckOut date,
RoomID int,
foreign key (RoomID) references ROOM(ID),
Username varchar(50),
FOREIGN KEY (Username)
REFERENCES ACCOUNT(Username)
)
go
create table ROOMIMG(
ID int primary key identity,
TypeID int,
foreign key (TypeID) references ROOMTYPE(ID),
Link varchar(max))
go

CREATE TABLE FORGOTPASS (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50),
	foreign key (Username) references account(username),
    ResetPass VARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL
)
go

CREATE PROCEDURE DeleteRecords
AS
BEGIN
    DELETE FROM FORGOTPASS
    WHERE CreatedAt < DATEADD(MINUTE, -1, GETDATE()); -- Xóa các bản ghi tạo trước 1 phút
END
go


insert into ROOMTYPE(Name,Price,Des,sumary)
values
('Standard Room', 200 ,'Discover comfort and simplicity in our Standard Room, designed for the modern traveler seeking a cozy retreat. Featuring a well-appointed space with tasteful decor, our Standard Room provides a welcoming atmosphere for both business and leisure guests. Unwind in a comfortable bed after a day of exploration or business meetings. The room is equipped with essential amenities, including a flat-screen TV, work desk, and complimentary Wi-Fi, ensuring you stay connected throughout your stay. The bathroom boasts modern fixtures and a refreshing shower. Enjoy the convenience of in-room coffee facilities and take a moment to relax in this thoughtfully designed space. Our Standard Room is a perfect choice for those who appreciate a blend of affordability and quality, providing a peaceful haven during your travels.','Discover comfort and simplicity in our Standard Room, designed for the modern traveler seeking a cozy retreat. Featuring a well-appointed space'),
('Deluxe Suite', 350 ,'Indulge in luxury and sophistication with our Deluxe Suite Room, a haven for those seeking an elevated experience. Step into a spacious retreat adorned with tasteful furnishings and modern amenities, where every detail is crafted for your comfort. The Deluxe Suite offers a separate living area, providing an expansive and private space for relaxation or casual meetings. Immerse yourself in the plush comfort of a king-sized bed and take advantage of the panoramic views from large windows. The suite is equipped with cutting-edge technology, including a large flat-screen TV, high-speed Wi-Fi, and convenient charging stations. The elegantly designed bathroom features premium fixtures, a rejuvenating bathtub, and complimentary designer toiletries. Indulge in the convenience of a well-stocked minibar and treat yourself to in-room dining for a truly luxurious experience. Whether traveling for business or leisure, our Deluxe Suite Room offers a refined retreat for the discerning traveler seeking both comfort and opulence.','Indulge in luxury and sophistication with our Deluxe Suite Room, a haven for those seeking an elevated experience. Step into a spacious retreat adorned with tasteful furnishings and modern amenities,'),
('Executive Room', 450 ,'Indulge in the epitome of luxury with our Executive Room, a haven designed for the sophisticated traveler. The Executive Room offers a harmonious blend of contemporary elegance and thoughtful functionality within a space that ranges from spacious to intimate. Unwind in the lavish comfort of a king-sized bed, adorned with premium linens, ensuring a restful night''s sleep.','Indulge in the epitome of luxury with our Executive Room, a haven designed for the sophisticated traveler.'),
('Panoramic Loft', 400 ,'<p>Indulge in the epitome of luxury with our Executive Room, a haven designed for the sophisticated traveler. The Executive Room offers a harmonious blend of contemporary elegance and thoughtful functionality within a space that ranges from spacious to intimate. Unwind in the lavish comfort of a king-sized bed, adorned with premium linens, ensuring a restful night&#39;s sleep.</p>

<p>Designed for both relaxation and productivity, the room features a well-appointed work area, high-speed Wi-Fi, and convenient charging stations. The stylish seating area invites you to unwind or engage in casual meetings. The upscale bathroom provides a sanctuary of tranquility, boasting premium amenities and a rejuvenating rain shower.</p>
','Indulge in the epitome of luxury with our Executive Room, a haven designed for the sophisticated traveler. The Executive Room offers a harmonious blend of contemporary'),
('Premier Room', 800 ,'<p>Welcome to the epitome of modern luxury in our Panoramic Loft Room&mdash;a unique sanctuary offering a distinctive blend of contemporary design and breathtaking views. This expansive loft-style accommodation is a statement in urban elegance, featuring floor-to-ceiling windows that frame stunning panoramic vistas of the city skyline.</p>

<p>The Panoramic Loft Room is designed for those seeking a truly immersive experience. With a spacious living area, elevated sleeping quarters, and stylish furnishings, this room captures the essence of sophisticated urban living. Relax in the comfortable seating area while taking in the dynamic cityscape or unwind in the king-sized bed, surrounded by chic decor and premium amenities.</p>

<p>The loft is equipped with state-of-the-art technology, including a large flat-screen TV, high-speed Wi-Fi, and convenient charging stations. The modern bathroom is a private oasis, complete with deluxe fixtures and a rejuvenating rain shower.</p>
','Welcome to the epitome of modern luxury in our Panoramic Loft Room&mdash;a unique sanctuary offering a distinctiv'),
('Suited Room', 700 ,'<p>Step into refinement and exclusivity with our Premier Room&mdash;an elegant retreat designed for those who appreciate the finer things in life. This thoughtfully curated space blends contemporary sophistication with timeless comfort to create a haven of unparalleled luxury.</p>

<p>The Premier Room welcomes you with a serene ambiance and meticulous attention to detail. Featuring a king-sized bed adorned with plush linens, this room offers a perfect balance of style and comfort. The well-appointed workspace, high-speed Wi-Fi, and convenient charging stations cater to the needs of both business and leisure travelers.</p>

<p>Indulge in the tastefully designed bathroom, equipped with upscale amenities and a rejuvenating rain shower. The Premier Room&#39;s expansive windows provide abundant natural light and offer captivating views of the surrounding environment.</p>
','Step into refinement and exclusivity with our Premier Room&mdash;an elegant retreat designed for those who appreciate the finer things in life.')

go



insert into ROOM(TypeID,Status)
values
(1,0),
(2,0),
(3,0),
(4,0),
(5,0),
(6,0),
(1,0),
(2,0),
(3,0),
(4,0),
(5,0),
(6,0),
(1,0),
(2,0),
(3,0),
(4,0),
(5,0),
(6,0),
(1,0),
(2,0),
(3,0),
(4,0),
(5,0),
(6,0),
(1,0),
(2,0),
(3,0),
(4,0),
(5,0),
(6,0),
(1,0),
(2,0),
(3,0)


go


INSERT INTO ROOMIMG (TypeID, Link)
VALUES 
(1, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_bedroom_880x475.jpg'),
(1, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_terrace_bedroom_880x475.jpg'),
(1, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_presidential_suite_livingroom_hero_880x475.jpg'),
(1, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_presidential_suite_livingroom_two_widows_880x475.jpg'),
(1, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_piazza_view_desk_880x475.jpg'),
(2, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_presidential_suite_bedroom_hero_880x475.jpg'),
(2, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_terrace_coffee_table_detail_880x475.jpg'),
(2, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_terrace_seating_880x475.jpg'),
(2, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_lobby_view-crpd-960x520-1.jpg'),
(2, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_fontana_delle_naiadi_evening_view_horizontal_880x475.jpg'),
(3, 'https://epscape.files.wordpress.com/2023/12/ananatara_palazzo_naiadi_rome_hotel_duplex_suite_bedroom_880x475.jpg'),
(3, 'https://epscape.files.wordpress.com/2023/12/ananatara_palazzo_naiadi_rome_hotel_duplex_suite_bathroom_880x475.jpg'),
(3, 'https://epscape.files.wordpress.com/2023/12/ananatara_palazzo_naiadi_rome_hoel_duplex_suite_living_880x475.jpg'),
(3, 'https://epscape.files.wordpress.com/2023/12/ananatara_palazzo_naiadi_rome_hotel_duplex_suite_stairs_880x475.jpg'),
(3, 'https://epscape.files.wordpress.com/2023/12/ananatara_palazzo_naiadi_rome_hotel_duplex_suite_table_view_880x475.jpg'),
(4, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_bedside_table_detail_880x475.jpg'),
(4, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_bedroom_880x475.jpg'),
(4, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_desk_detail_880x475.jpg'),
(4, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_piazza_view_balcony_champagne_880x475.jpg'),
(5, 'https://epscape.files.wordpress.com/2023/12/paris-2016-suite-junior-suite-01.webp'),
(5, 'https://epscape.files.wordpress.com/2023/12/paris-2016-suite-couture-suite-02.webp'),
(5, 'https://epscape.files.wordpress.com/2023/12/paris-2016-suite-couture-suite-01.webp'),
(5, 'https://epscape.files.wordpress.com/2023/12/paris-2016-room-royale-oriental-01.webp'),
(5, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_piazza_view_bed_with_view_880x475.jpg'),
(6, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_piazza_view_bed_seating_view_880x475.jpg'),
(6, 'https://epscape.files.wordpress.com/2023/12/anantara_palazzo_naiadi_rome_hotel_junior_suite_piazza_view_bed_with_view_880x475.jpg'),
(6, 'https://epscape.files.wordpress.com/2023/12/paris-room-superior-room-banquette.webp'),
(6, 'https://epscape.files.wordpress.com/2023/12/paris-suite-deluxe-suite.webp')

go

update ROOMTYPE set Sumary = 'Indulge in luxury and sophistication with our Deluxe Suite Room, a haven for those seeking an elevated experience.'where id = 2


go