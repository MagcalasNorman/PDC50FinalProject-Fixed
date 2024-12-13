<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"), true); // Decode as associative array

// Validate input
if (!isset($data['name'], $data['gender'], $data['contactNo'], $data['email'], $data['attendanceRecords'], $data['course'], $data['yearLevel'])) {
    echo json_encode(["message" => "Invalid input data"]);
    exit;
}

$name = $conn->real_escape_string($data['name']);
$gender = $conn->real_escape_string($data['gender']);
$contactNo = $conn->real_escape_string($data['contactNo']);
$email = $conn->real_escape_string($data['email']);
$attendanceRecords = $conn->real_escape_string($data['attendanceRecords']);
$course = $conn->real_escape_string($data['course']);
$yearLevel = $conn->real_escape_string($data['yearLevel']);

// Begin transaction
$conn->begin_transaction();

try {
    // Insert into tblstudent
    $sqlStudent = "INSERT INTO tblstudent (Name, Gender, ContactNo, Email, AttendanceRecords, Course, YearLevel) 
                   VALUES ('$name', '$gender', '$contactNo', '$email', '$attendanceRecords', '$course', '$yearLevel')";
    if (!$conn->query($sqlStudent)) {
        throw new Exception("Error inserting into tblstudent: " . $conn->error);
    }

    // Commit transaction
    $conn->commit();
    echo json_encode(["message" => "User added successfully"]);
} catch (Exception $e) {
    // Rollback transaction on error
    $conn->rollback();
    echo json_encode(["message" => $e->getMessage()]);
}

$conn->close();
?>
