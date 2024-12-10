<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"));

$id = $data->id;
$name = $data->name;
$gender = $data->gender;
$contactNo = $data->contactNo;
$email = $data->email;
$course = $data->course;
$gradeLevel = $data->gradeLevel;
$attendanceRecords = $data->attendanceRecords;

$sql = "UPDATE tblstudent SET Name='$name', Gender='$gender', ContactNo='$contactNo', Email='$email', Course='$course', GradeLevel='$gradeLevel', AttendanceRecords='$attendanceRecords' WHERE ID=$id";

if ($conn->query($sql) === TRUE) {
    echo json_encode(["message" => "User updated successfully"]);
} else {
    echo json_encode(["message" => "Error: " . $conn->error]);
}

$conn->close();
?>