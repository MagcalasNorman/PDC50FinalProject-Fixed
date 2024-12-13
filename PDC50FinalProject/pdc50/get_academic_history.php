<?php
include 'db.php';

header("Content-Type: application/json");

$data = json_decode(file_get_contents("php://input"));
$studentId = $data->studentId;

if (!$studentId) {
    echo json_encode(["message" => "Student ID is required"]);
    exit();
}

$sql = "SELECT Course, yearLevel FROM academic_history WHERE StudentID = ?";
$stmt = $conn->prepare($sql);
$stmt->bind_param("i", $studentId);

if ($stmt->execute()) {
    $result = $stmt->get_result();
    $academicHistory = [];

    while ($row = $result->fetch_assoc()) {
        $academicHistory[] = $row;
    }

    echo json_encode($academicHistory);
} else {
    echo json_encode(["message" => "Error retrieving academic history: " . $conn->error]);
}

$stmt->close();
$conn->close();
?>
