<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"), true); // Decode as associative array

// Check if studentId is passed
if (!isset($data['studentId'])) {
    echo json_encode(["message" => "Invalid input data"]);
    exit;
}

$studentId = $conn->real_escape_string($data['studentId']);

// Query the academic_history table
$sql = "SELECT id, StudentID, Course, yearLevel FROM academic_history WHERE StudentID = '$studentId'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    $academicHistory = [];
    while ($row = $result->fetch_assoc()) {
        $academicHistory[] = [
            "ID" => $row['id'],  // Ensure 'id' is used here
            "StudentID" => $row['StudentID'],
            "Course" => $row['Course'],
            "YearLevel" => $row['yearLevel']
        ];
    }
    echo json_encode($academicHistory);
} else {
    echo json_encode([]); // Return empty array if no records
}

$conn->close();
?>
