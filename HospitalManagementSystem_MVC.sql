

create database HospitalManagementSystem_MVC;

use HospitalManagementSystem_MVC


create table Admin_Profile(
	AdminId int primary key identity,
	FullName nvarchar(50) not null,
	Email nvarchar(50) not null check(Email like '%@gmail.com'),
	--Password nvarchar(50) not null,
	Contact bigint check(Contact >= 6000000000 AND Contact <= 9999999999),
	--Address nvarchar(max),
	DOB datetime not null,
	Age as FLOOR(DATEDIFF(DAY, DOB, GETDATE()) / 365.25), -- Adding Computed column
	Gender varchar(10) check(Gender in ('Male', 'Female')),
	AdminImage nvarchar(max),
	CreatedAt datetime DEFAULT GETDATE(),
	UpdatedAt datetime DEFAULT GETDATE()
);

drop table Admin_Profile
truncate table Admin_Profile

select * from Admin_Profile

insert into Admin_Profile(FullName, Email, Contact, DOB, Gender, AdminImage)
values('Harrison wells', 'harrisonwells@gmail.com', 6543872915, '1970-05-18', 'Male', 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716708794/Doctors%20Images%20for%20MVC%20Project/images_ivuexa.jpg')

select * from Admin_Profile


--------------------

create table Doctors_Profiles(
	DoctorId int primary key identity,
	FullName nvarchar(50) not null,
	Email nvarchar(50) not null unique,
	constraint chk_email check(Email like '%@gmail.com'),
	--Password nvarchar(50) not null,
	Contact bigint unique check(Contact >= 6000000000 AND Contact <= 9999999999),
	--Address nvarchar(max),
	DOB datetime not null,
	Age as FLOOR(DATEDIFF(DAY, DOB, GETDATE()) / 365.25), -- Adding Computed column
	Gender varchar(10) check(Gender in ('Male', 'Female')),
	Qualification nvarchar(max) not null,
	Specialization nvarchar(100) not null,
	constraint chk_specialization check(Specialization in ('Cardiologist', 'Dermatology', 'Psychiatry', 'Nephrologists', 'Surgery')),
	Experience int not null,
	DoctorImage nvarchar(max),
	IsTrash bit default 0,
	CreatedAt datetime DEFAULT GETDATE(),
	UpdatedAt datetime DEFAULT GETDATE()
);

drop table Doctors_Profiles
truncate table Doctors_Profiles

select * from Doctors_Profiles

INSERT INTO Doctors_Profiles (FullName, Email, Contact, DOB, Gender, Qualification, Specialization, Experience, DoctorImage)
VALUES 
('Dr. John Smith', 'johnsmith@gmail.com', 8456375286, '1970-01-15', 'Male', 'MBBS, MD', 'Cardiologist', 15, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716708134/Doctors%20Images%20for%20MVC%20Project/images_t4vorw.jpg'),
('Dr. Jane Doe', 'janedoe@gmail.com', 7245286483, '1980-02-20', 'Female', 'MBBS, MD', 'Dermatology', 10, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716707892/Doctors%20Images%20for%20MVC%20Project/slider-1.1_khlpww.png'),
('Dr. Emily Clark', 'emilyclark@gmail.com', 9657823145, '1975-03-25', 'Female', 'MBBS, MD', 'Psychiatry', 12, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716707892/Doctors%20Images%20for%20MVC%20Project/an-indian-young-female-doctor-isolated-on-green-ai-generated-photo_jl0p7d.jpg'),
('Dr. Michael Brown', 'michaelbrown@gmail.com', 6458293648, '1985-04-30', 'Male', 'MBBS, MD', 'Nephrologists', 8, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716708310/Doctors%20Images%20for%20MVC%20Project/medical-equipment-loan-for-doctors-vp_llaggg.jpg'),
('Dr. Sarah Davis', 'sarahdavis@gmail.com', 8456286716, '1990-05-05', 'Female', 'MBBS, MD', 'Surgery', 5, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716707892/Doctors%20Images%20for%20MVC%20Project/profile-photo-attractive-family-doc-600nw-1724693776_p2x8xj.jpg')


select * from Doctors_Profiles

--('Dr. David Wilson', 'davidwilson@gmail.com', 7234168365, '1965-06-10', 'Male', 'MBBS, MD', 'Cardiologist', 20, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716707892/Doctors%20Images%20for%20MVC%20Project/indian-male-doctor_szghjc.jpg'),
--('Dr. Laura Garcia', 'lauragarcia@gmail.com', 9456824517, '1978-07-15', 'Female', 'MBBS, MD', 'Dermatology', 11, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716707892/Doctors%20Images%20for%20MVC%20Project/cadqt_y8hn1m.jpg'),
--('Dr. Robert Martinez', 'robertmartinez@gmail.com', 8467245184, '1982-08-20', 'Male', 'MBBS, MD', 'Psychiatry', 9, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716707892/Doctors%20Images%20for%20MVC%20Project/360_F_260040900_oO6YW1sHTnKxby4GcjCvtypUCWjnQRg5_pgenqb.jpg'),
--('Dr. Maria Hernandez', 'mariahernandez@gmail.com', 6473825782, '1987-09-25', 'Female', 'MBBS, MD', 'Nephrologists', 6, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716708254/Doctors%20Images%20for%20MVC%20Project/images_v78hyx.jpg'),
--('Dr. James Lopez', 'jameslopez@gmail.com', 7452857135, '1972-10-30', 'Male', 'MBBS, MD', 'Surgery', 18, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716708214/Doctors%20Images%20for%20MVC%20Project/a-mature-indian-male-doctor-on-a-white-background-ai-generated-photo_apcku1.jpg');

--------- InsertDoctor

alter proc usp_InsertDoctor(
@FullName nvarchar(50),
@Email nvarchar(50),
@Contact bigint,
@DOB datetime,
@Gender varchar(10),
@Qualification nvarchar(max),
@Specialization nvarchar(100),
@Experience int,
@DoctorImage nvarchar(max)
)
as
begin
	set nocount on;
	--begin try
	--begin transaction
	--	if not exists(select 1 from Doctors_Profiles where Email = @Email)
	--	begin
			INSERT INTO Doctors_Profiles (FullName, Email, Contact, DOB, Gender, Qualification, Specialization, Experience, DoctorImage)
			VALUES(@FullName, @Email, @Contact, @DOB, @Gender, @Qualification, @Specialization, @Experience, @DoctorImage);
	--	end
	--	else
	--	RAISERROR('Error in Insert Doctor b/z Doctor already exxis', 16, 1);
	--commit transaction
	--end try
	--begin catch
	--	--if @@TRANCOUNT > 0
	--	--	Rollback transaction;
	--	--declare @ErrorMessage nvarchar(4000) = ERROR_MESSAGE();
	--	--declare @ErrorSeverity int = ERROR_SEVERITY();
	--	--declare @ErrorStatus int = ERROR_STATe();
	--	--RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorStatus);
	--	print ERROR_MESSAGE();
	--	THROW;
	--end catch
end;

exec usp_InsertDoctor 'Dr. David Wilson', 'davidwilson212@gmail.com', 7234168382, '1965-06-10', 'Male', 'MBBS, MD', 'Cardiologist', 20, 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716707892/Doctors%20Images%20for%20MVC%20Project/indian-male-doctor_szghjc.jpg'

select * from Doctors_Profiles

----- FetchAllDoctors

alter proc usp_FetchAllDoctors
as
begin
	select * from Doctors_Profiles;
end

exec usp_FetchAllDoctors


------------- GetDoctorById

create proc usp_GetDoctorById
@DoctorId int
as
begin
	set nocount on;
	begin try
		select * from Doctors_Profiles where DoctorId = @DoctorId;
		if (@@ROWCOUNT > 0)
		begin
			print 'Found';
		end
		else
		RAISERROR(N'Doctor not found for requested id: %d', 16, 1, @DoctorId);
	end try
	begin catch
		print ERROR_MESSAGE();
		THROW;
	end catch
end;

exec usp_GetDoctorById 2

select * from Doctors_Profiles

---------  UpdateDoctor

alter proc usp_UpdateDoctor(
@DoctorId  int,
@FullName nvarchar(50),
@Email nvarchar(50),
@Contact bigint,
@DOB datetime,
@Gender varchar(10),
@Qualification nvarchar(max),
@Specialization nvarchar(100),
@Experience int,
@DoctorImage nvarchar(max)
)
as
begin
	set nocount on;
	update Doctors_Profiles set FullName = @FullName, Email = @Email, Contact = @Contact, DOB = @DOB, Gender = @Gender, Qualification = @Qualification,
		Specialization = @Specialization, Experience = @Experience, DoctorImage = @DoctorImage, UpdatedAt = GETDATE() where DoctorId = @DoctorId;
	--if(@@ROWCOUNT = 0)
	--	RAISERROR(N'Doctor not found for id: %d', 16, 1, @DoctorId);
	--	Return;
end;

drop proc usp_UpdateDoctor 

exec usp_UpdateDoctor @DoctorId = 1, @FullName = 'Doctor-1', @Email = 'doctor1@gmail.com', @Contact = 8765434567, @DOB = '1999-2-2', @Gender = 'Female',
	@Qualification = 'MD', @Specialization = 'Dermatology', @Experience = 6, @DoctorImage = 'doctor1-image'

select * from Doctors_Profiles

------------ Delete Doctor

alter proc usp_DeleteDoctor
@DoctorId int
as
begin
	delete from Doctors_Profiles where DoctorId = @DoctorId;
end;

exec usp_DeleteDoctor 14

select * from Doctors_Profiles

------ DoctorLogin

alter proc usp_DoctorLogin(
@DoctorId int,
@FullName nvarchar(50)
)
as
begin
	select * from Doctors_Profiles where  DoctorId = @DoctorId and FullName  = @FullName;
	if (@@Rowcount = 0)
		RAISERROR('Invalid credentials', 16, 1);
end;

exec usp_DoctorLogin 11, 'Dr. David Wilson'

select * from Doctors_Profiles

'============================== Patients_Profiles ==========================================='

create table Patients_Profiles(
	PatientId int primary key identity,
	FullName nvarchar(50) not null,
	Email nvarchar(50) not null unique check(Email like '%@gmail.com'),
	--Password nvarchar(50) not null,
	Contact bigint unique check(Contact >= 6000000000 AND Contact <= 9999999999),
	--Address nvarchar(max),
	DOB date not null,
	Age AS DATEDIFF(YEAR, DOB, GETDATE()) - CASE 
                                                WHEN MONTH(DOB) > MONTH(GETDATE()) 
                                                  OR (MONTH(DOB) = MONTH(GETDATE()) AND DAY(DOB) > DAY(GETDATE())) 
                                                THEN 1 
                                                ELSE 0 
                                              END,
	Gender varchar(10) check(Gender in ('Male', 'Female')),
	PatientImage nvarchar(max),
	IsTrash bit default 0,
	CreatedAt datetime DEFAULT GETDATE(),
	UpdatedAt datetime DEFAULT GETDATE()
);

drop table Patients_Profiles
truncate table Patients_Profiles

select * from Patients_Profiles

delete from Patients_Profiles where PatientId=7

INSERT INTO Patients_Profiles (FullName, Email, Contact, DOB, Gender, PatientImage)
VALUES 
('Clark jane', 'clarkjane@gmail.com', 7528684563, '1970-01-01', 'Male', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSuMk0lM9b9l1YC3SxpBSyCEzh9cFhwmjN9rA&usqp=CAU'),
('Allen Roy', 'allenroy@gmail.com', 8648372452, '1985-02-02', 'Female', 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716710886/Doctors%20Images%20for%20MVC%20Project/Patients%20Images%20for%20MVC%20Project/images_ryqlfd.jpg'),
('Alice Green', 'alicegreen@gmail.com', 8231459657, '1992-03-03', 'Female', 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716710823/Doctors%20Images%20for%20MVC%20Project/Patients%20Images%20for%20MVC%20Project/images_la0vth.jpg'),
('Bob White', 'bobwhite@gmail.com', 9364864582, '1988-04-04', 'Male', 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716711075/Doctors%20Images%20for%20MVC%20Project/Patients%20Images%20for%20MVC%20Project/images_s2ncne.jpg'),
('Charlie Black', 'charlieblack@gmail.com', 6716845628, '1975-05-05', 'Male', 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716711054/Doctors%20Images%20for%20MVC%20Project/Patients%20Images%20for%20MVC%20Project/images_us0lvm.jpg')


select * from Patients_Profiles


--('Emily Brown', 'emilybrown@gmail.com', 8365723416, '1993-06-06', 'Female', 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716710793/Doctors%20Images%20for%20MVC%20Project/Patients%20Images%20for%20MVC%20Project/images_b4rvcv.jpg'),
--('Miller Garry', 'millergarry@gmail.com', 6824517945, '1987-07-07', 'Male', 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716711008/Doctors%20Images%20for%20MVC%20Project/Patients%20Images%20for%20MVC%20Project/images_rvdtc6.jpg'),
--('Fiona Clark', 'fionaclark@gmail.com', 7245184846, '1991-08-08', 'Female', 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716710772/Doctors%20Images%20for%20MVC%20Project/Patients%20Images%20for%20MVC%20Project/images_ncy1dp.jpg'),
--('George Harris', 'georgeharris@gmail.com', 8257826473, '1983-09-09', 'Male', 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716710742/Doctors%20Images%20for%20MVC%20Project/Patients%20Images%20for%20MVC%20Project/lhbamh2mntqfckt7oson.jpg'),
--('Hannah Martin', 'hannahmartin@gmail.com', 7137452855, '1994-10-10', 'Female', 'https://res.cloudinary.com/dsuqerssy/image/upload/v1716711363/Doctors%20Images%20for%20MVC%20Project/Patients%20Images%20for%20MVC%20Project/images_eqaqnq.jpg');


alter proc usp_InsertPatient(
@FullName nvarchar(50),
@Email nvarchar(50),
@Contact bigint,
@DOB datetime,
@Gender varchar(10),
@PatientImage nvarchar(max)
)
as
begin
	set nocount on;
	begin try
		if not exists(select 1 from Patients_Profiles where Email = @Email)
			begin
				insert into Patients_Profiles(FullName, Email, Contact, DOB, Gender, PatientImage)
				values (@FullName, @Email, @Contact, CONVERT(date, @DOB) , @Gender, @PatientImage);
			end
		else
		RAISERROR(N'Patient alredy registered for email: %s', 16, 1, @Email);
	end try
	begin catch
		print ERROR_MESSAGE();
		throw;
	end catch
end;

exec usp_InsertPatient 'Clark jane', 'clarkjane@gmail.com', 7528684563, '1970-01-01 12:3:9', 'Male', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSuMk0lM9b9l1YC3SxpBSyCEzh9cFhwmjN9rA&usqp=CAU'

select * from Patients_Profiles

truncate table Patients_Profiles

---------- GetAllPatients

create proc usp_GetAllPatients
as
begin
	select * from Patients_Profiles;
end

exec usp_GetAllPatients

-------- GetPatientById

create proc usp_GetPatientById
@PatientId int
as
begin
	select * from Patients_Profiles where PatientId = @PatientId;
end

usp_GetPatientById 1

-------- UpdatePatient

alter proc usp_UpdatePatient(
@PatientId int,
@FullName nvarchar(50),
@Email nvarchar(50),
@Contact bigint,
@DOB datetime,
@Gender varchar(10),
@PatientImage nvarchar(max)
)
as
begin
	update Patients_Profiles set FullName = @FullName, Email = @Email, Contact = @Contact, DOB = @DOB, Gender = @Gender,
		PatientImage = @PatientImage, UpdatedAt = GETDATE() where PatientId = @PatientId;
end;

exec usp_UpdatePatient 2, 'Patient-11', 'patient11@gmail.com', 8765432145, '1980-04-04', 'Female', 'Patient11-image'

select * from Patients_Profiles

----- DeletePatient

create proc usp_DeletePatient
@PatientId int
as
begin
	update Patients_Profiles set IsTrash = IsTrash where PatientId = @PatientId;
end;

exec usp_DeletePatient 4

select * from Patients_Profiles

------ PermanentDeletePatient

alter proc usp_PermanentDeletePatient
@PatientId int = null
as
begin
	if (@PatientId is null)
		delete from Patients_Profiles where IsTrash = 1;
	else if not exists(select 1 from Patients_Profiles where PatientId = @PatientId)
		RAISERROR(N'Falied to Permanent delete patient account b/z account id: %d does not exist', 16, 1, @PatientId);
	else
	begin
		declare @Trash bit;
		select @Trash = IsTrash from Patients_Profiles where PatientId = @PatientId;
		if (@Trash = 1)
			delete from Patients_Profiles where PatientId = @PatientId;
		else
		RAISERROR(N'Falied to Permanent delete patient account b/z account id: %d is active', 16, 1, @PatientId);
	end
end;

exec usp_PermanentDeletePatient 1

select * from Patients_Profiles


--- PatientLogin

alter proc usp_PatientLogin(
@PatientId int,
@FullName nvarchar(50)
)
as
begin
	select * from Patients_Profiles where PatientId = @PatientId and FullName = @FullName;
	if(@@Rowcount >0 )
		print 'Login success';
	else RAISERROR(N'Login failed', 16, 1);
end;

exec usp_PatientLogin 6, 'Clark jane1'

------- --------------------------------


declare @Date date;
set @Date = GETDATE();
select @Date as date

declare @DateTime datetime;
set @DateTime = GETDATE();
select @DateTime as datetime

declare @Time time;
set @Time = GETDATE();
select @Time as time

select 'hello' as text

select 10+20 as sum



-----------------------------------------

--CREATE TABLE Schedules (
--    ScheduleID INT PRIMARY KEY IDENTITY,
--    ScheduleName NVARCHAR(100) NOT NULL,
--    StartTime TIME NOT NULL,
--    EndTime TIME NOT NULL
--);

--drop table Schedules
--truncate table Schedules

--select * from Schedules

--INSERT INTO Schedules (ScheduleName, StartTime, EndTime)
--VALUES 
--('Morning Shift 1', '09:00:00', '10:00:00'),
--('Morning Shift 2', '10:00:00', '11:00:00'),
--('Afternoon Shift 1', '12:00:00', '13:00:00'),
--('Afternoon Shift 2', '13:00:00', '14:00:00'),
--('Afternoon Shift 3', '15:00:00', '16:00:00'),
--('Afternoon Shift 4', '16:00:00', '17:00:00'),
--('Evening Shift 1', '18:00:00', '19:00:00'),
--('Evening Shift 2', '19:00:00', '20:00:00')

--select * from Schedules

declare @Date date;
set @Date = '2024-5-31 6:30:0';
select @Date as date
--if(@Date >= convert(date, GETDATE()))
if(@Date > GETDATE())
print 'true'
else
print 'false'

-------------------------------------------

CREATE TABLE Appointments (
    AppointmentId INT PRIMARY KEY IDENTITY,
    PatientId INT,
    DoctorId INT,
	ConcernAbout nvarchar(max),
    AppointmentDate DATE NOT NULL,
    StartTime Time not null,
	EndTime Time not null,
    CONSTRAINT FK_Appointment_Patient FOREIGN KEY (PatientId) REFERENCES Patients_Profiles(PatientId),
    CONSTRAINT FK_Appointment_Doctor FOREIGN KEY (DoctorId) REFERENCES Doctors_Profiles(DoctorId),
	CONSTRAINT CHK_StartTime_BusinessHours CHECK (StartTime >= '10:00' AND StartTime <= '18:00')
);

drop table Appointments
truncate table Appointments

select * from Appointments

-------- BookAppointment

alter proc usp_BookAppointment(
@PatientId INT,
@DoctorId INT,
@ConcernAbout nvarchar(max),
@AppointmentDate DATE,
@StartTime Time
)
as
begin
	SET NOCOUNT ON;
	if exists (select 1 from Patients_Profiles where PatientId = @PatientId and IsTrash = 0)
	begin
		if exists (select 1 from Doctors_Profiles where DoctorId = @DoctorId  and IsTrash = 0)
		begin
			if( (@AppointmentDate > CONVERT(date, GETDATE()) and @StartTime between '10:00' and '18:00')  OR (@AppointmentDate = CONVERT(date, GETDATE()) and @StartTime between '10:00' and '18:00' and @StartTime > CONVERT(time, GETDATE())) )
			begin
				if not exists (select 1 from Appointments where DoctorId = @DoctorId and AppointmentDate = @AppointmentDate and (@StartTime < EndTime AND DATEADD(MINUTE, 60, @StartTime) > StartTime)) 
					INSERT INTO Appointments (PatientId, DoctorId, ConcernAbout , AppointmentDate, StartTime, EndTime)
					VALUES(@PatientId, @DoctorId, @ConcernAbout, @AppointmentDate, @StartTime, DATEADD(MINUTE, 60, @StartTime));
				else RAISERROR(N'Falied to book appointment b/z slot is not free', 16, 1);
			end
			else RAISERROR(N'Falied to book appointment b/z invalid time slot', 16, 1);
		end
		else RAISERROR(N'Falied to book appointment b/z doctor account id: %d does not exist', 16, 1, @DoctorId);
	end
	else RAISERROR(N'Falied to book appointment b/z patient account id: %d does not exist', 16, 1, @PatientId);
end;


drop proc usp_MakeAppointment

exec usp_BookAppointment 6, 11, 'Routine check-up', '2024-06-01', '14:00'
exec usp_BookAppointment 8,19, 'Heart health consultation',  '2024-06-3', '11:30'
exec usp_BookAppointment 9, 20, 'Consultation for back pain', '2024-06-2', '14:00'


select * from Doctors_Profiles   -- 11

select * from Patients_Profiles  -- 6

select * from Appointments

------ GetAllAppointments

create proc usp_GetAllAppointments
as
begin
	select * from Appointments;
end

exec usp_GetAllAppointments

truncate table Appointments

select * from Appointments


---- 10th Review  -> Task problem => to display patient details along with appointed doctor-name & doctor-image

alter proc usp_PatientDetailsWithAppointedDoctor
as
begin
	select a.AppointmentId as AppID, 
	p.*, 
	d.DoctorId as DID, 
	d.FullName as DoctorName, 
	d.DoctorImage as DImage
	from Appointments a join Patients_Profiles p on a.PatientId = p.PatientId
	join Doctors_Profiles d on a.DoctorId = d.DoctorId
	where p.IsTrash = 0 and d.IsTrash = 0;
end;

exec usp_PatientDetailsWithAppointedDoctor

select * from Doctors_Profiles 
select * from Patients_Profiles 
select * from Appointments



---------------------------------------------------------------------------------
In the RAISERROR function in SQL Server, the numbers 16 and 1 refer to the severity level and the state, respectively.

Severity Level (16):
The severity level indicates the type and severity of the error.
A severity level of 16 is used for general user errors. It indicates that there is a problem that the user can correct (e.g., data validation errors).

State (1):
The state is an integer from 0 to 255 that indicates the location or the part of the code where the error occurred. It's used to provide additional information about the error's source.
A state value of 1 is commonly used when there is no specific need to distinguish between different points of failure within the same error.

---------------------------------------------------------------------------------------------












INSERT INTO Appointments (PatientId, DoctorId, ConcernAbout, AppointmentDate, StartTime, EndTime)
VALUES
(1, 1, 'Routine check-up', '2024-05-28', 1, 1),
(2, 2, 'Consultation for back pain', '2024-05-30', 2, 1),
(3, 3, 'Follow-up visit', '2024-06-01', 3, 1),
(4, 4, 'Flu symptoms', '2024-06-02', 4, 1),
(5, 5, 'Allergy testing', '2024-06-02', 5, 1)


select * from Appointments


--(6, 6, 'Physical exam', '2024-06-03', 6, 1),
--(7, 7, 'Blood pressure check', '2024-06-03', 7, 1),
--(8, 8, 'Cholesterol screening', '2024-06-04', 8, 1),
--(9, 9, 'Diabetes management', '2024-06-04', 9, 1),
--(10, 10, 'Heart health consultation', '2024-06-05', 10, 1);







