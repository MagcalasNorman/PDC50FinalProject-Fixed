<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"), true); // Decode as associative array

// Validate input
if (!isset($data['id'], $data['name'], $data['gender'], $data['contactNo'], $data['email'], $data['attendanceRecords'], $data['course'], $data['yearLevel'])) {
    echo json_encode(["message" => "Invalid input data"]);
    exit;
}

$id = (int) $data['id']; // Ensure ID is an integer
$name = $conn->real_escape_string($data['name']);
$gender = $conn->real_escape_string($data['gender']);
$contactNo = $conn->real_escape_string($data['contactNo']);
$email = $conn->real_escape_string($data['email']);
$attendanceRecords = $conn->real_escape_string($data['attendanceRecords']);
$newCourse = $conn->real_escape_string($data['course']);
$newYearLevel = $conn->real_escape_string($data['yearLevel']);

// Begin transaction
$conn->begin_transaction();

try {
    // Fetch the current Course and YearLevel before updating
    $sqlFetch = "SELECT Course, YearLevel FROM tblstudent WHERE ID = $id";
    $result = $conn->query($sqlFetch);
    if (!$result || $result->num_rows == 0) {
        throw new Exception("Student not found with ID $id");
    }
    $currentData = $result->fetch_assoc();
    $oldCourse = $currentData['Course'];
    $oldYearLevel = $currentData['YearLevel'];

    // Save old Course and YearLevel to academic_history
    $sqlHistory = "INSERT INTO academic_history (StudentID, Course, yearLevel) 
                   VALUES ('$id', '$oldCourse', '$oldYearLevel')";
    if (!$conn->query($sqlHistory)) {
        throw new Exception("Error inserting into academic_history: " . $conn->error);
    }

    // Update tblstudent with new data
    $sqlStudent = "UPDATE tblstudent 
                   SET Name='$name', Gender='$gender', ContactNo='$contactNo', Email='$email', AttendanceRecords='$attendanceRecords', 
                       Course='$newCourse', YearLevel='$newYearLevel' 
                   WHERE ID=$id";
    if (!$conn->query($sqlStudent)) {
        throw new Exception("Error updating tblstudent: " . $conn->error);
    }

    // Commit transaction
    $conn->commit();
    echo json_encode(["message" => "User updated successfully"]);
} catch (Exception $e) {
    // Rollback transaction on error
    $conn->rollback();
    echo json_encode(["message" => $e->getMessage()]);
}

$conn->close();
?>
