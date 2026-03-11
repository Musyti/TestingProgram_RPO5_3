#pragma once

#include <string>
#include <vector>
#include <iostream>
#include <algorithm>
#include "nlohmann/json.hpp"
using json = nlohmann::json;

/**
 * Класс Question представляет вопрос в викторине
 * Может содержать как простой текст, так и несколько вариантов ответа
 */
class Question {
private:
    // Содержание вопроса - может быть строкой или массивом строк (варианты ответов)
    std::string content_;  // Унифицированное хранение как вектор строк
    std::vector<std::string> options_;
    std::vector<int> correct_options_;
    bool is_correct_;                       // Правильность ответа пользователя
    bool is_resolved_;                    // Флаг решен ли вопрос
    int points_;                          // Количество очков за вопрос
    std::string explanation_;                        // Объяснение правильного ответа


public:
    // Конструкторы
    Question();
    explicit Question(const std::string& content);

    // Геттеры и сеттеры
    std::string getContent() const;
    std::string getContentAsString() const;  // Получить как единую строку
    void setContent(const std::string& content);

    bool isCorrect() const;
    void setCorrect(bool correct);

    bool isResolved() const;
    void setResolved(bool resolved);

    int getPoints() const;
    void setPoints(int points);

    /**
     * Подсчитывает количество очков за вопрос
     * @return количество начисленных очков
     */
    int countPoints();

    /**
     * Редактирует вопрос
     */
    void edit();

    /**
     * Проверяет, является ли вопрос вопросом с множественным выбором
     * @return true если есть варианты ответов (больше одного элемента в content_)
     */
    bool isMultipleChoice() const;

    /**
     * Добавляет вариант ответа (для множественного выбора)
     * @param option вариант ответа
     */
    void addOption(const std::string& option);

    /**
     * Удаляет вариант ответа
     * @param index индекс удаляемого варианта
     */
    void removeOption(int index);
    json toJson() const;
    void fromJson(const json& j);
};