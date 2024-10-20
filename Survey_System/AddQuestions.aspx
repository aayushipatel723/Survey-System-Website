<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuestions.aspx.cs" Inherits="Survey_System.AddQuestions" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="AddQuestions.css" type="text/css" rel="stylesheet" />
    <title>Add Questions</title>
    <script type="text/javascript">
        let questionCount = 0;

        function setQuestionCount() {
            questionCount = document.getElementById('NumberOfQuestions').value;
            document.getElementById('questionsContainer').innerHTML = ''; // Clear previous questions
            for (let i = 0; i < questionCount; i++) {
                addQuestion(i);
            }
        }

        function addQuestion(index) {
            const questionHtml = `
                <div>
                    <label for="QuestionText${index}">Question ${index + 1}:</label>
                    <input type="text" id="QuestionText${index}" name="QuestionText${index}" />
                    <label for="FieldType${index}">Field Type:</label>
                    <select id="FieldType${index}" name="FieldType${index}" onchange="toggleOptions(${index})">
                        <option value="TextBox">TextBox</option>
                    </select>
                    <div id="OptionsContainer${index}" style="display:none;">
                        <label for="NumberOfOptions${index}">Number of Options:</label>
                        <input type="number" id="NumberOfOptions${index}" name="NumberOfOptions${index}" min="0" oninput="updateOptions(${index})" />
                        <div id="OptionsList${index}"></div>
                    </div>
                </div>
            `;
            document.getElementById('questionsContainer').innerHTML += questionHtml;
        }

        function toggleOptions(index) {
            const fieldType = document.getElementById(`FieldType${index}`).value;
            const optionsContainer = document.getElementById(`OptionsContainer${index}`);
            optionsContainer.style.display = (fieldType === 'RadioButton' || fieldType === 'CheckBox') ? 'block' : 'none';
        }

        function updateOptions(index) {
            const numberOfOptions = document.getElementById(`NumberOfOptions${index}`).value;
            const optionsList = document.getElementById(`OptionsList${index}`);
            optionsList.innerHTML = ''; // Clear previous options

            for (let i = 0; i < numberOfOptions; i++) {
                optionsList.innerHTML += `
                    <div>
                        <label for="OptionText${index}_${i}">Option ${i + 1}:</label>
                        <input type="text" id="OptionText${index}_${i}" name="OptionText${index}_${i}" />
                    </div>
                `;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="NumberOfQuestions">Number of Questions:</label>
            <input type="number" id="NumberOfQuestions" name="NumberOfQuestions" min="1" onchange="setQuestionCount()" />
            <div id="questionsContainer"></div>
        </div>
        <div>
            <button type="submit">Save Questions</button>
        </div>
    </form>
</body>
</html>
