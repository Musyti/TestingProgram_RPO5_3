#pragma once

#include <string>
#include <vector>
#include "Question.h"
#include <iostream>
#include <fstream>
#include "nlohmann/json.hpp"
using json = nlohmann::json;

/**
 * Класс Category представляет категорию вопросов в викторине
 */
class Category {
private:
    std::string name_;              // Название категории
    int points_;                     // Количество очков за правильный ответ
    std::vector<Question> questions_; // Массив вопросов категории
    bool is_finished_;               // Флаг завершения категории
    bool is_active_;                 // Флаг активности категории

public:
    // Конструкторы
    Category();
    explicit Category(const std::string& name);
    ~Category();

    // Геттеры и сеттеры (опционально)
    std::string getName() const;
    void setName(const std::string& name);

    int getPoints() const;
    void setPoints(int points);

    bool isFinished() const;
    bool isActive() const;

    /**
     * Отображает информацию о категории
     */
    void showInfo();

    /**
     * Запускает категорию
     */
    void start();

    /**
     * Переходит к следующему вопросу
     */
    void nextQuestion();

    /**
     * Возвращается к предыдущему вопросу
     */
    void previousQuestion();

    /**
     * Завершает категорию и возвращает набранные очки
     * @return количество набранных очков
     */
    int end();

    /**
     * Добавляет новый вопрос в категорию
     */
    void addQuestion();

    /**
     * Удаляет вопрос по индексу
     * @param index индекс удаляемого вопроса
     */
    void removeQuestion(int index);

    /**
     * Загружает категорию из файла
     * @param filename имя файла для загрузки
     */
    json toJson() const;
    void fromJson(const json& j);

    // Перегруженные методы файлового ввода/вывода
    void loadFromFile(const std::string& filename);
    void saveToFile(const std::string& filename);

    // Для обратной совместимости (если нужны старые названия)
    void saveInFile(const std::string& filename) { saveToFile(filename); }
    bool validateJsonSchema(const json& j);
};