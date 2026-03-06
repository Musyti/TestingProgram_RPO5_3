#include "Question.h"

// Конструктор по умолчанию
Question::Question()
    : content_()
    , correct_(false)
    , is_resolved_(false)
    , points_(0) {
}

// Конструктор с текстовым вопросом
Question::Question(const std::string& content)
    : content_({ content })  // Создаем вектор с одним элементом
    , correct_(false)
    , is_resolved_(false)
    , points_(0) {
}

// Конструктор с вариантами ответов
Question::Question(const std::vector<std::string>& content)
    : content_(content)
    , correct_(false)
    , is_resolved_(false)
    , points_(0) {
}

// Геттеры и сеттеры
std::vector<std::string> Question::getContent() const {
    return content_;
}

std::string Question::getContentAsString() const {
    if (content_.empty()) {
        return "";
    }

    // Если это простой вопрос (один элемент)
    if (content_.size() == 1) {
        return content_[0];
    }

    // Если это вопрос с вариантами, объединяем их
    std::string result = content_[0] + "\nВарианты ответов:\n";
    for (size_t i = 1; i < content_.size(); ++i) {
        result += std::to_string(i) + ". " + content_[i] + "\n";
    }
    return result;
}

void Question::setContent(const std::string& content) {
    content_.clear();
    content_.push_back(content);
}

void Question::setContent(const std::vector<std::string>& content) {
    content_ = content;
}

bool Question::isCorrect() const {
    return correct_;
}

void Question::setCorrect(bool correct) {
    correct_ = correct;
}

bool Question::isResolved() const {
    return is_resolved_;
}

void Question::setResolved(bool resolved) {
    is_resolved_ = resolved;
}

int Question::getPoints() const {
    return points_;
}

void Question::setPoints(int points) {
    points_ = points;
}

/**
 * Подсчитывает количество очков за вопрос
 */
int Question::countPoints() {
    if (!is_resolved_) {
        std::cout << "Вопрос еще не решен!" << std::endl;
        return 0;
    }

    if (correct_) {
        std::cout << "Ответ правильный! Начислено очков: " << points_ << std::endl;
        return points_;
    }
    else {
        std::cout << "Ответ неправильный. Очки не начислены." << std::endl;
        return 0;
    }
}

/**
 * Редактирует вопрос
 */
void Question::edit() {
    std::cout << "=== Редактирование вопроса ===" << std::endl;

    if (content_.empty()) {
        std::cout << "Вопрос пуст. Добавьте текст вопроса." << std::endl;
        std::string newContent;
        std::getline(std::cin, newContent);
        content_.push_back(newContent);
    }
    else {
        std::cout << "Текущий вопрос: " << getContentAsString() << std::endl;
        std::cout << "Введите новый текст вопроса (или оставьте пустым для сохранения текущего):" << std::endl;

        if (isMultipleChoice()) {
            std::cout << "Это вопрос с вариантами ответов." << std::endl;
            std::cout << "Количество вариантов: " << content_.size() - 1 << std::endl;

            // Редактирование основного вопроса
            std::string newQuestion;
            std::cout << "Текст вопроса [" << content_[0] << "]: ";
            std::getline(std::cin, newQuestion);
            if (!newQuestion.empty()) {
                content_[0] = newQuestion;
            }

            // Редактирование вариантов
            for (size_t i = 1; i < content_.size(); ++i) {
                std::string newOption;
                std::cout << "Вариант " << i << " [" << content_[i] << "]: ";
                std::getline(std::cin, newOption);
                if (!newOption.empty()) {
                    content_[i] = newOption;
                }
            }
        }
        else {
            // Простой текстовый вопрос
            std::string newContent;
            std::cout << "Текст вопроса [" << content_[0] << "]: ";
            std::getline(std::cin, newContent);
            if (!newContent.empty()) {
                content_[0] = newContent;
            }
        }
    }

    std::cout << "Введите количество очков за вопрос [" << points_ << "]: ";
    std::string pointsInput;
    std::getline(std::cin, pointsInput);
    if (!pointsInput.empty()) {
        try {
            points_ = std::stoi(pointsInput);
        }
        catch (...) {
            std::cout << "Ошибка ввода очков. Оставлено прежнее значение." << std::endl;
        }
    }

    std::cout << "Вопрос успешно отредактирован!" << std::endl;
}

/**
 * Проверяет, является ли вопрос вопросом с множественным выбором
 */
bool Question::isMultipleChoice() const {
    return content_.size() > 1;
}

/**
 * Добавляет вариант ответа
 */
void Question::addOption(const std::string& option) {
    content_.push_back(option);
    std::cout << "Вариант ответа добавлен. Всего вариантов: " << content_.size() - 1 << std::endl;
}

/**
 * Удаляет вариант ответа
 */
void Question::removeOption(int index) {
    if (content_.size() <= 1) {
        std::cout << "Ошибка: Вопрос должен содержать хотя бы текст вопроса!" << std::endl;
        return;
    }

    // Индекс в векторе смещен на 1, так как content_[0] - это текст вопроса
    int vectorIndex = index + 1;

    if (vectorIndex > 0 && vectorIndex < static_cast<int>(content_.size())) {
        content_.erase(content_.begin() + vectorIndex);
        std::cout << "Вариант ответа " << index << " удален." << std::endl;
    }
    else {
        std::cout << "Ошибка: Неверный индекс варианта ответа!" << std::endl;
    }
}