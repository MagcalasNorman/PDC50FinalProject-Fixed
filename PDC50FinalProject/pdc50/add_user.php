<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"));

$name = $data->name;
$gender = $data->gender;
$contactNo = $data->contactNo;
$email = $data->email;
$course = $data->course;
$gradeLevel = $data->gradeLevel;
$attendanceRecords = $data->attendanceRecords;
$sql = "INSERT INTO tblstudent (Name, Gender, ContactNo, Email, Course, GradeLevel, AttendanceRecords) VALUES ('$name', '$gender', '$contactNo', '$email', '$course', '$gradeLevel', '$attendanceRecords')";

if ($conn->query($sql) === TRUE) {
    echo json_encode(["message" => "User added successfully"]);
} else {
    echo json_encode(["message" => "Error: " . $conn->error]);
}

$conn->close();
?>
