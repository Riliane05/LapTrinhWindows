
-- Add extended properties (mô tả cột) cho bảng __MigrationHistory
BEGIN
    DECLARE @columns_MH TABLE (ColumnName NVARCHAR(100), Description NVARCHAR(500))
    INSERT INTO @columns_MH (ColumnName, Description)
    VALUES
    ('MigrationId', N'Mã định danh của bản ghi migration'),
    ('ContextKey', N'Tên đầy đủ của lớp DbContext dùng cho migration'),
    ('Model', N'Mô hình cơ sở dữ liệu đã được mã hóa dạng nhị phân'),
    ('ProductVersion', N'Phiên bản của Entity Framework sử dụng để tạo migration')

    DECLARE @col_MH NVARCHAR(100), @desc_MH NVARCHAR(500), @sql_MH NVARCHAR(MAX)
    DECLARE cur_MH CURSOR FOR SELECT ColumnName, Description FROM @columns_MH
    OPEN cur_MH
    FETCH NEXT FROM cur_MH INTO @col_MH, @desc_MH
    WHILE @@FETCH_STATUS = 0
    BEGIN
        BEGIN TRY
            IF EXISTS (
                SELECT * FROM fn_listextendedproperty(NULL, 'schema', 'dbo', 'table', '__MigrationHistory', 'column', @col_MH)
            )
                SET @sql_MH = '
                EXEC sp_updateextendedproperty 
                    @name = N''MS_Description'', 
                    @value = N''' + @desc_MH + ''',
                    @level0type = N''SCHEMA'', @level0name = N''dbo'',
                    @level1type = N''TABLE'',  @level1name = N''__MigrationHistory'',
                    @level2type = N''COLUMN'', @level2name = N''' + @col_MH + ''';'
            ELSE
                SET @sql_MH = '
                EXEC sp_addextendedproperty 
                    @name = N''MS_Description'', 
                    @value = N''' + @desc_MH + ''',
                    @level0type = N''SCHEMA'', @level0name = N''dbo'',
                    @level1type = N''TABLE'',  @level1name = N''__MigrationHistory'',
                    @level2type = N''COLUMN'', @level2name = N''' + @col_MH + ''';'
            EXEC (@sql_MH)
        END TRY
        BEGIN CATCH
            PRINT ERROR_MESSAGE()
        END CATCH
        FETCH NEXT FROM cur_MH INTO @col_MH, @desc_MH
    END
    CLOSE cur_MH
    DEALLOCATE cur_MH
END
GO

-- Add extended properties cho bảng Departments
BEGIN
    DECLARE @columns_DP TABLE (ColumnName NVARCHAR(100), Description NVARCHAR(500))
    INSERT INTO @columns_DP (ColumnName, Description)
    VALUES
    ('Id', N'Mã phòng ban'),
    ('Name', N'Tên phòng ban')

    DECLARE @col_DP NVARCHAR(100), @desc_DP NVARCHAR(500), @sql_DP NVARCHAR(MAX)
    DECLARE cur_DP CURSOR FOR SELECT ColumnName, Description FROM @columns_DP
    OPEN cur_DP
    FETCH NEXT FROM cur_DP INTO @col_DP, @desc_DP
    WHILE @@FETCH_STATUS = 0
    BEGIN
        BEGIN TRY
            IF EXISTS (
                SELECT * FROM fn_listextendedproperty(NULL, 'schema', 'dbo', 'table', 'Departments', 'column', @col_DP)
            )
                SET @sql_DP = '
                EXEC sp_updateextendedproperty 
                    @name = N''MS_Description'', 
                    @value = N''' + @desc_DP + ''',
                    @level0type = N''SCHEMA'', @level0name = N''dbo'',
                    @level1type = N''TABLE'',  @level1name = N''Departments'',
                    @level2type = N''COLUMN'', @level2name = N''' + @col_DP + ''';'
            ELSE
                SET @sql_DP = '
                EXEC sp_addextendedproperty 
                    @name = N''MS_Description'', 
                    @value = N''' + @desc_DP + ''',
                    @level0type = N''SCHEMA'', @level0name = N''dbo'',
                    @level1type = N''TABLE'',  @level1name = N''Departments'',
                    @level2type = N''COLUMN'', @level2name = N''' + @col_DP + ''';'
            EXEC (@sql_DP)
        END TRY
        BEGIN CATCH
            PRINT ERROR_MESSAGE()
        END CATCH
        FETCH NEXT FROM cur_DP INTO @col_DP, @desc_DP
    END
    CLOSE cur_DP
    DEALLOCATE cur_DP
END
GO

-- Add extended properties (mô tả cột) cho bảng Employees
BEGIN
    DECLARE @columns_Emp TABLE (ColumnName NVARCHAR(100), Description NVARCHAR(500))
    INSERT INTO @columns_Emp (ColumnName, Description)
    VALUES
    ('Id', N'Mã định danh của nhân viên'),
    ('HoTen', N'Họ và tên nhân viên'),
    ('NgaySinh', N'Ngày sinh của nhân viên'),
    ('GioiTinh', N'Giới tính nhân viên'),
    ('DiaChi', N'Địa chỉ nơi ở'),
    ('Email', N'Địa chỉ email liên hệ'),
    ('SDT', N'Số điện thoại liên hệ'),
    ('VanHoa', N'Trình độ văn hóa'),
    ('ChuyenMon', N'Trình độ chuyên môn'),
    ('MaNhanVien', N'Mã số nhân viên nội bộ'),
    ('ChucVu', N'Chức vụ đảm nhiệm'),
    ('TrangThai', N'Trạng thái hoạt động'),
    ('Luong', N'Mức lương cơ bản'),
    ('PhuCap', N'Phụ cấp kèm theo'),
    ('PhongBanID', N'Mã phòng ban liên kết'),
    ('Avatar', N'Đường dẫn hình đại diện'),
    ('CheckInTime', N'Thời điểm chấm công vào'),
    ('CheckOutTime', N'Thời điểm chấm công ra')

    DECLARE @col_Emp NVARCHAR(100), @desc_Emp NVARCHAR(500), @sql_Emp NVARCHAR(MAX)
    DECLARE cur_Emp CURSOR FOR SELECT ColumnName, Description FROM @columns_Emp
    OPEN cur_Emp
    FETCH NEXT FROM cur_Emp INTO @col_Emp, @desc_Emp
    WHILE @@FETCH_STATUS = 0
    BEGIN
        BEGIN TRY
            IF EXISTS (
                SELECT * FROM fn_listextendedproperty(NULL, 'schema', 'dbo', 'table', 'Employees', 'column', @col_Emp)
            )
                SET @sql_Emp = '
                EXEC sp_updateextendedproperty 
                    @name = N''MS_Description'', 
                    @value = N''' + @desc_Emp + ''',
                    @level0type = N''SCHEMA'', @level0name = N''dbo'',
                    @level1type = N''TABLE'',  @level1name = N''Employees'',
                    @level2type = N''COLUMN'', @level2name = N''' + @col_Emp + ''';'
            ELSE
                SET @sql_Emp = '
                EXEC sp_addextendedproperty 
                    @name = N''MS_Description'', 
                    @value = N''' + @desc_Emp + ''',
                    @level0type = N''SCHEMA'', @level0name = N''dbo'',
                    @level1type = N''TABLE'',  @level1name = N''Employees'',
                    @level2type = N''COLUMN'', @level2name = N''' + @col_Emp + ''';'
            EXEC (@sql_Emp)
        END TRY
        BEGIN CATCH
            PRINT ERROR_MESSAGE()
        END CATCH
        FETCH NEXT FROM cur_Emp INTO @col_Emp, @desc_Emp
    END
    CLOSE cur_Emp
    DEALLOCATE cur_Emp
END
GO

-- Add extended properties (mô tả cột) cho bảng TimekeepingRecords
BEGIN
    DECLARE @columns_Time TABLE (ColumnName NVARCHAR(100), Description NVARCHAR(500))
    INSERT INTO @columns_Time (ColumnName, Description)
    VALUES
    ('Id', N'Mã định danh bản ghi chấm công'),
    ('EmployeeId', N'Mã nhân viên được chấm công'),
    ('Date', N'Ngày chấm công'),
    ('CheckInTime', N'Thời điểm vào làm'),
    ('CheckOutTime', N'Thời điểm tan ca')

    DECLARE @col_Time NVARCHAR(100), @desc_Time NVARCHAR(500), @sql_Time NVARCHAR(MAX)
    DECLARE cur_Time CURSOR FOR SELECT ColumnName, Description FROM @columns_Time
    OPEN cur_Time
    FETCH NEXT FROM cur_Time INTO @col_Time, @desc_Time
    WHILE @@FETCH_STATUS = 0
    BEGIN
        BEGIN TRY
            IF EXISTS (
                SELECT * FROM fn_listextendedproperty(NULL, 'schema', 'dbo', 'table', 'TimekeepingRecords', 'column', @col_Time)
            )
                SET @sql_Time = '
                EXEC sp_updateextendedproperty 
                    @name = N''MS_Description'', 
                    @value = N''' + @desc_Time + ''',
                    @level0type = N''SCHEMA'', @level0name = N''dbo'',
                    @level1type = N''TABLE'',  @level1name = N''TimekeepingRecords'',
                    @level2type = N''COLUMN'', @level2name = N''' + @col_Time + ''';'
            ELSE
                SET @sql_Time = '
                EXEC sp_addextendedproperty 
                    @name = N''MS_Description'', 
                    @value = N''' + @desc_Time + ''',
                    @level0type = N''SCHEMA'', @level0name = N''dbo'',
                    @level1type = N''TABLE'',  @level1name = N''TimekeepingRecords'',
                    @level2type = N''COLUMN'', @level2name = N''' + @col_Time + ''';'
            EXEC (@sql_Time)
        END TRY
        BEGIN CATCH
            PRINT ERROR_MESSAGE()
        END CATCH
        FETCH NEXT FROM cur_Time INTO @col_Time, @desc_Time
    END
    CLOSE cur_Time
    DEALLOCATE cur_Time
END
GO

-- Add extended properties (mô tả cột) cho bảng Users
BEGIN
    DECLARE @columns_Users TABLE (ColumnName NVARCHAR(100), Description NVARCHAR(500))
    INSERT INTO @columns_Users (ColumnName, Description)
    VALUES
    ('Id', N'Mã định danh tài khoản người dùng'),
    ('TenTK', N'Tên đăng nhập của người dùng'),
    ('MatKhau', N'Mật khẩu đăng nhập (nên được mã hóa khi lưu trữ)'),
    ('LoaiTK', N'Loại tài khoản (Quản lý hoặc Nhân viên)')

    DECLARE @col_Users NVARCHAR(100), @desc_Users NVARCHAR(500), @sql_Users NVARCHAR(MAX)
    DECLARE cur_Users CURSOR FOR SELECT ColumnName, Description FROM @columns_Users
    OPEN cur_Users
    FETCH NEXT FROM cur_Users INTO @col_Users, @desc_Users
    WHILE @@FETCH_STATUS = 0
    BEGIN
        BEGIN TRY
            IF EXISTS (
                SELECT * FROM fn_listextendedproperty(NULL, 'schema', 'dbo', 'table', 'Users', 'column', @col_Users)
            )
                SET @sql_Users = '
                EXEC sp_updateextendedproperty 
                    @name = N''MS_Description'', 
                    @value = N''' + @desc_Users + ''',
                    @level0type = N''SCHEMA'', @level0name = N''dbo'',
                    @level1type = N''TABLE'',  @level1name = N''Users'',
                    @level2type = N''COLUMN'', @level2name = N''' + @col_Users + ''';'
            ELSE
                SET @sql_Users = '
                EXEC sp_addextendedproperty 
                    @name = N''MS_Description'', 
                    @value = N''' + @desc_Users + ''',
                    @level0type = N''SCHEMA'', @level0name = N''dbo'',
                    @level1type = N''TABLE'',  @level1name = N''Users'',
                    @level2type = N''COLUMN'', @level2name = N''' + @col_Users + ''';'
            EXEC (@sql_Users)
        END TRY
        BEGIN CATCH
            PRINT ERROR_MESSAGE()
        END CATCH
        FETCH NEXT FROM cur_Users INTO @col_Users, @desc_Users
    END
    CLOSE cur_Users
    DEALLOCATE cur_Users
END
GO

