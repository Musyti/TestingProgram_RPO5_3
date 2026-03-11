#include <iostream>
#include <string>
#include <vector>
#include "Administrator.h"
#include "MainMenu.h"

class Question {
private:
    std::string text;
    std::string answer;

public:
    Question(std::string qText = "", std::string qAnswer = "") : text(qText), answer(qAnswer) {}

    std::string getText() const { return text; }
    std::string getAnswer() const { return answer; }

    void setText(std::string newText) { text = newText; }
    void setAnswer(std::string newAnswer) { answer = newAnswer; }

    void display() const {
        std::cout << "Вопрос: " << text << std::endl;
        std::cout << "Ответ: " << answer << std::endl;
    }
};

class Theme {
private:
    std::string name;
    std::vector<Question> questions;

public:
    Theme(std::string themeName = "") : name(themeName) {}

    std::string getName() const { return name; }

    void addQuestion(const Question& q) {
        questions.push_back(q);
        std::cout << "Вопрос добавлен в тему \"" << name << "\"" << std::endl;
    }

    void removeQuestion(int index) {
        if (index >= 0 && index < static_cast<int>(questions.size())) {
            questions.erase(questions.begin() + index);
            std::cout << "Вопрос удален из темы \"" << name << "\"" << std::endl;
        }
        else {
            std::cout << "Ошибка: неверный индекс вопроса!" << std::endl;
        }
    }

    void editQuestion(int index, const std::string& newText, const std::string& newAnswer) {
        if (index >= 0 && index < static_cast<int>(questions.size())) {
            questions[index].setText(newText);
            questions[index].setAnswer(newAnswer);
            std::cout << "Вопрос отредактирован в теме \"" << name << "\"" << std::endl;
        }
        else {
            std::cout << "Ошибка: неверный индекс вопроса!" << std::endl;
        }
    }

    void displayQuestions() const {
        if (questions.empty()) {
            std::cout << "В теме \"" << name << "\" нет вопросов." << std::endl;
            return;
        }

        std::cout << "\n=== Вопросы темы \"" << name << "\" ===" << std::endl;
        for (size_t i = 0; i < questions.size(); i++) {
            std::cout << i + 1 << ". ";
            questions[i].display();
            std::cout << "-------------------" << std::endl;
        }
    }

    int getQuestionCount() const { return static_cast<int>(questions.size()); }
};

class TopicManager {
private:
    std::vector<Theme> themes;
    Administrator admin;
    MainMenu menu;

public:
    void createTheme() {
        std::string themeName;
        std::cout << "Введите название новой темы: ";
        std::cin.ignore();
        std::getline(std::cin, themeName);

        themes.push_back(Theme(themeName));
        std::cout << "Тема \"" << themeName << "\" успешно создана!" << std::endl;
    }

    void deleteTheme() {
        if (themes.empty()) {
            std::cout << "Нет доступных тем для удаления." << std::endl;
            return;
        }

        displayThemes();
        int choice;
        std::cout << "Выберите номер темы для удаления: ";
        std::cin >> choice;

        if (choice > 0 && choice <= static_cast<int>(themes.size())) {
            std::cout << "Тема \"" << themes[choice - 1].getName() << "\" удалена." << std::endl;
            themes.erase(themes.begin() + choice - 1);
        }
        else {
            std::cout << "Ошибка: неверный номер темы!" << std::endl;
        }
    }

    void addQuestionToTheme() {
        if (themes.empty()) {
            std::cout << "Сначала создайте тему!" << std::endl;
            return;
        }

        displayThemes();
        int themeChoice;
        std::cout << "Выберите номер темы для добавления вопроса: ";
        std::cin >> themeChoice;

        if (themeChoice > 0 && themeChoice <= static_cast<int>(themes.size())) {
            std::cin.ignore();

            std::string qText, qAnswer;
            std::cout << "Введите текст вопроса: ";
            std::getline(std::cin, qText);
            std::cout << "Введите ответ на вопрос: ";
            std::getline(std::cin, qAnswer);

            themes[themeChoice - 1].addQuestion(Question(qText, qAnswer));
        }
        else {
            std::cout << "Ошибка: неверный номер темы!" << std::endl;
        }
    }

    void removeQuestionFromTheme() {
        if (themes.empty()) {
            std::cout << "Нет доступных тем." << std::endl;
            return;
        }

        displayThemes();
        int themeChoice;
        std::cout << "Выберите номер темы: ";
        std::cin >> themeChoice;

        if (themeChoice > 0 && themeChoice <= static_cast<int>(themes.size())) {
            themes[themeChoice - 1].displayQuestions();

            if (themes[themeChoice - 1].getQuestionCount() > 0) {
                int qChoice;
                std::cout << "Выберите номер вопроса для удаления: ";
                std::cin >> qChoice;

                themes[themeChoice - 1].removeQuestion(qChoice - 1);
            }
        }
        else {
            std::cout << "Ошибка: неверный номер темы!" << std::endl;
        }
    }

    void editQuestionInTheme() {
        if (themes.empty()) {
            std::cout << "Нет доступных тем." << std::endl;
            return;
        }

        displayThemes();
        int themeChoice;
        std::cout << "Выберите номер темы: ";
        std::cin >> themeChoice;

        if (themeChoice > 0 && themeChoice <= static_cast<int>(themes.size())) {
            themes[themeChoice - 1].displayQuestions();

            if (themes[themeChoice - 1].getQuestionCount() > 0) {
                int qChoice;
                std::cout << "Выберите номер вопроса для редактирования: ";
                std::cin >> qChoice;

                std::cin.ignore();
                std::string newText, newAnswer;
                std::cout << "Введите новый текст вопроса: ";
                std::getline(std::cin, newText);
                std::cout << "Введите новый ответ на вопрос: ";
                std::getline(std::cin, newAnswer);

                themes[themeChoice - 1].editQuestion(qChoice - 1, newText, newAnswer);
            }
        }
        else {
            std::cout << "Ошибка: неверный номер темы!" << std::endl;
        }
    }

    void displayThemes() const {
        if (themes.empty()) {
            std::cout << "Нет доступных тем." << std::endl;
            return;
        }

        std::cout << "\n=== Список тем ===" << std::endl;
        for (size_t i = 0; i < themes.size(); i++) {
            std::cout << i + 1 << ". " << themes[i].getName()
                << " (вопросов: " << themes[i].getQuestionCount() << ")" << std::endl;
        }
    }

    void run() {
        int choice;

        do {
            menu.display();
            std::cout << "Выберите действие: ";
            std::cin >> choice;

            switch (choice) {
            case 1:
                createTheme();
                break;
            case 2:
                deleteTheme();
                break;
            case 3:
                addQuestionToTheme();
                break;
            case 4:
                removeQuestionFromTheme();
                break;
            case 5:
                editQuestionInTheme();
                break;
            case 6:
                displayThemes();
                break;
            case 7:
                admin.showAdminInfo();
                break;
            case 0:
                std::cout << "Программа завершена." << std::endl;
                break;
            default:
                std::cout << "Ошибка: неверный выбор!" << std::endl;
            }

            std::cout << std::endl;

        } while (choice != 0);
    }
};

int main() {
    setlocale(LC_ALL, "ru");

    std::cout << "=== Система управления темами и вопросами ===" << std::endl;
    std::cout << "Подключены файлы: Administrator.h и MainMenu.h" << std::endl << std::endl;

    TopicManager manager;
    manager.run();

    return 0;
}